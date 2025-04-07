using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.U2D;
using TMPro;
using UnityEngine.SceneManagement;
using System.Data;

public class DamageEvent : MonoBehaviour
{
    public UnityEvent damageEvent;
    // Start is called before the first frame update
    //Initialize variables
    public SpriteRenderer sprite;
    //Animation curve variable
    [SerializeField]
    public AnimationCurve hitReg;
    public ZombieSpawner spawner;
    public GameObject zombieSpawner;
    public GameObject GAMEOVER;
    //Variable for the health bar text
    public TextMeshProUGUI text;
    Color changeColor = new Color(0, 0, 0, 255);
    float colorCurve;
    bool hit;
    int counter;
    float timer = 0.5f;
    public float t;
    Coroutine damaged;
    //Health variables
    public float playerHP = 100f, playerMaxHP = 100f;
    //int respawnCounter = 0;
    void Start()
    {
        damageEvent.AddListener(Hit);
    }

    // Update is called once per frame
    void Update()
    {
        //Set the text to the hp
        text.text = playerHP + "/" + playerMaxHP;
        //Get the zombie spawner component
        spawner = zombieSpawner.GetComponent<ZombieSpawner>();
        //Go through the list of spawned zombies to find the one that hits the player
        for (int i = 0; i < spawner.spawnedZombies.Count; i++)
        {
            //Checks if the zombie exists
            if (spawner.spawnedZombies[i] != null)
            {
                //Initiaize player position and zombie position
                Vector3 zombiePos = spawner.spawnedZombies[i].transform.position;

                Vector3 playerPos = transform.position;
                //Code which calculates the position of the zombie and player for a hitbox
                if (zombiePos.x <= playerPos.x + 0.8f &&
                    zombiePos.x >= playerPos.x - 0.8f &&
                    zombiePos.y <= playerPos.y + 0.8f &&
                    zombiePos.y >= playerPos.y - 0.8f)
                {
                    if (!hit)
                    {
                        damageEvent.Invoke();
                        colorCurve = 0f;
                        changeColor = Color.red;
                    }
                    //Debug.Log(playerHP);
                }
            }
        }
        if (playerHP <= 0)
        {
            playerHP = 100f;
        }
        /*Color color = new Color(0, 0, 0, 255);
        if (hit)
        {
            //Debug.Log("Changing Sprite " + colorCurve + " " + changeColor);
            //If hit fade from red to whitea
            colorCurve = color.r + Time.deltaTime;
            changeColor.b += hitReg.Evaluate(colorCurve);
            changeColor.g += hitReg.Evaluate(colorCurve);
            sprite.color = changeColor;
           // counter++;
        }
        else
        {
            //Sets everything to default when the timer ends
            colorCurve = 0f;
            changeColor = Color.red;
            //counter = 0;
            //hit = false;
            sprite.color = new Color(1, 1, 1);
        }*/
    }

    public void Hit()
    {
        Debug.Log("Hit");
        //Enable hit boolean
        //Sets the colors to default
        colorCurve = 0f;
        changeColor = Color.red;
        sprite.color = new Color(255, 255, 255);
        hit = true;
        playerHP -= spawner.zombieDamage;
        if (playerHP <= 0)
        {
            playerHP = 0;
            //Add game over screen
            //gameObject.SetActive(false);
            //SpriteRenderer newSprite = gameObject.GetComponent<SpriteRenderer>()
            Time.timeScale = 0;
            GAMEOVER.SetActive(true);
            
        }

        damaged = StartCoroutine(ImmunityFrames());
    }

    private IEnumerator ImmunityFrames()
    {
        t = 0;
        Color color = new Color(0, 0, 0, 255);
        while (t < timer)
        {
            colorCurve = color.r + Time.deltaTime;
            changeColor.b += hitReg.Evaluate(colorCurve);
            changeColor.g += hitReg.Evaluate(colorCurve);
            sprite.color = changeColor;
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Completed IFrames");
        colorCurve = 0f;
        changeColor = Color.red;
        //counter = 0;
        //hit = false;
        sprite.color = new Color(1, 1, 1);
        hit = false;
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("System");
    }
}
