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
    public GameObject player;
    //Animation curve variable
    [SerializeField]
    public AnimationCurve hitReg;
    //Variable for the bullet spawner game object
    public GameObject bulletSpawner;
    public BulletSpawner spawner;
    public ZombieSpawner zombieSpawner;
    //Variable for the health bar text
    public TextMeshProUGUI text;
    private Vector3 playerPos;
    Color changeColor = new Color(0,0,0,255);
    Color originalColor = new Color(0, 0.5f, 0);
    float colorCurve;
    bool hit;
    int counter;
    int sound;

    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;

    public LevelSystem levelSystem;

    float timer = 1f;
    public float t;
    bool timerUp = false;

    public float speed = 2f;
    //Health variables
    public float zombieHP = 100f, zombieMaxHP = 100f;
    public float zombieDamage = 1f;
    //int respawnCounter = 0;
    void Start()
    {
        //Initialize zombies stats off of zombie spawner
        bulletSpawner = GameObject.Find("BulletSpawner");
        player = GameObject.Find("Player");
        levelSystem = GameObject.Find("Level").GetComponent<LevelSystem>();
        zombieHP = zombieSpawner.zombieHP;
        zombieMaxHP = zombieSpawner.zombieMaxHP;
        zombieDamage = zombieSpawner.zombieDamage;
        speed = zombieSpawner.speed;
        //Play the zombie sound whenever a zombie spawns
        StartCoroutine(SoundTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //Make the zombie move
        zombieMovement();
        if (timerUp)
        {
            playSound();
        }
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
            levelSystem.xp += Mathf.FloorToInt((zombieMaxHP * zombieDamage * speed) / 30);
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
    public void zombieMovement()
    {
        //Constantly move the zombie towards the player's position based on their speed and time
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }

    public void playSound()
    {
        //Choose between 1 of 3 zombie sounds to play randomly
        sound = Random.Range(1, 4);
        Debug.Log(sound);
        if (sound == 1)
        {
            sound1.Play();
        }
        if (sound == 2)
        {
            sound2.Play();
        }
        if (sound == 3)
        {
            sound3.Play();
        }
        timerUp = false;
    }

    private IEnumerator SoundTimer()
    {
        //Cooldown for sound
        t = 0;
        while (t < timer)
        {
            t += Time.deltaTime;
            yield return null;
        }
        timerUp = true;
    }
}
