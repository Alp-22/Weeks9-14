using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    // Start is called before the first frame update
    //Initialize variables
    public SpriteRenderer sprite;
    //Animation curve variable
    [SerializeField]
    public AnimationCurve hitReg;
    //Variable for the bullet spawner game object
    public GameObject bulletSpawner;
    public BulletSpawner spawner;
    public ZombieSpawner zombieSpawner;
    //Variable for the health bar text
    public TextMeshProUGUI text;
    Color changeColor = new Color(0,0,0,255);
    Color originalColor = new Color(0, 0.5f, 0);
    float colorCurve;
    bool hit;
    int counter;
    //Health variables
    float zombieHP = 100f, zombieMaxHP = 100f;
    //int respawnCounter = 0;
    void Start()
    {
        bulletSpawner = GameObject.Find("BulletSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        //Set the text to the hp
        text.text = zombieHP + "/" + zombieMaxHP;
        //Get the bullet spawner component
        spawner = bulletSpawner.GetComponent<BulletSpawner>();
        //Go through the list of spawned bullets to find the one that hits the zombie
        for (int i = 0; i < spawner.spawnedBullets.Count; i++)
        {
            //Checks if the bullet exists
            if (spawner.spawnedBullets[i] != null)
            {
                //Initiaize zombie position and bullet position
                Vector3 bulletPos = spawner.spawnedBullets[i].transform.position;

                Vector3 zombiePos = transform.position;
                //Code which calculates the position of the bullet and zombie for a hitbox
                if (bulletPos.x <= zombiePos.x + 0.8f &&
                    bulletPos.x >= zombiePos.x - 0.8f &&
                    bulletPos.y <= zombiePos.y + 0.8f &&
                    bulletPos.y >= zombiePos.y - 0.8f)
                {
                    //spawner.bullet.enabled = false;
                    //Minus the zombie HP by damage
                    zombieHP -= spawner.bulletDamage;
                    //Destroy the bullet that hit the zombie
                    Destroy(spawner.spawnedBullets[i]);
                    Debug.Log("Hit");
                    //Enable hit boolean
                    hit = true;
                    //Sets the colors to default
                    colorCurve = 0f;
                    changeColor = Color.red;
                    counter = 0;
                    //sprite.color = new Color(0, 0, 0, 255);
                    //Debug.Log(zombieHP);
                }
            }
        }
        if (zombieHP <= 0)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        /*if (gameObject.activeSelf == false)
        {
            respawnCounter++;
            Debug.Log(respawnCounter);
            if (respawnCounter > 1000)
            {
                gameObject.SetActive(true);
                zombieHP = 1000f;
                respawnCounter = 0;
            }
        }    */
        Color color = new Color(0, 0, 0, 255);
        if (hit && counter<=255)
        {
            //If hit fade from red to white
            colorCurve = color.r + Time.deltaTime;
            //changeColor.b += hitReg.Evaluate(colorCurve);
            changeColor.r -= hitReg.Evaluate(colorCurve);
            changeColor.g += hitReg.Evaluate(colorCurve) / 2.3f;
            sprite.color = changeColor;
            counter++;
        }
        else
        {
            //Sets everything to default when the timer ends
            colorCurve = 0f;
            //changeColor = Color.red;
            counter = 0;
            hit = false;
            sprite.color = originalColor;
      
        }
    }
}
