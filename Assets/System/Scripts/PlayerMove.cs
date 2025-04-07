using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public DamageEvent damageListener;
    public TrailRenderer trailrenderer;
    void Start()
    {
        //DamageEvent = GetComponent<DamageEvent>();
        //Initializes the trail renderer
        trailrenderer = GetComponent<TrailRenderer>();
    }
    //Initialize variables
    public float timer = 1f, cooldownTimer = 5f;
    public float t, t2;
    bool cooldownUp = true;
    float speed = 5f;
    float timerCooldown = 5f;
    public TextMeshProUGUI text;
    public AudioSource dashAudio;

    // Update is called once per frame
    void Update()
    {
        //Adjust player speed
        //float speed = 0.005f;
        //Vector2 playerPos = transform.position;
        //If W is pressed move up
        /*if (Input.GetKey(KeyCode.W))
        {
            //Stops the player from going out of bounds
            if (playerPos.y <= (Camera.main.orthographicSize * Screen.width / Screen.height) / 2)
            {
                playerPos.y += speed;
            }

        }
        //If S is pressed move down
        if (Input.GetKey(KeyCode.S))
        {
            //Stops the player from going out of bounds
            if (playerPos.y >= -(Camera.main.orthographicSize * Screen.width / Screen.height) / 2)
            {
                playerPos.y -= speed;
            }
        }
        //If A is pressed move left
        if (Input.GetKey(KeyCode.A))
        {
            //Stops the player from going out of bounds
            if (playerPos.x >= -(Camera.main.orthographicSize * Screen.width / Screen.height))
            {
                playerPos.x -= speed;
            }
        }
        //If D is pressed move right
        if (Input.GetKey(KeyCode.D))
        {
            //Stops the player from going out of bounds
            if (playerPos.x <= (Camera.main.orthographicSize * Screen.width / Screen.height))
            {
                playerPos.x += speed;
            }
        }
        transform.position = playerPos;*/

        Vector3 playerPos = transform.position;
        float direction = Input.GetAxis("Horizontal");
        float direction2 = Input.GetAxis("Vertical");

        playerPos += transform.right * direction * speed * Time.deltaTime;
        playerPos += transform.up * direction2 * speed * Time.deltaTime;
        //If you're off cooldown and you press shift you go faster
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownUp)
        {
            speed *= 3;
            //Starts your dash timer
            StartCoroutine(DashTimer());
        }
        //Prevents you from going out of bounds
        if (transform.position.x > 50)
        {
            playerPos.x = 50;
        }
        if (transform.position.x < -50)
        {
            playerPos.x = -50;
        }
        if (transform.position.y > 50)
        {
            playerPos.y = 50;
        }
        if (transform.position.y < -50)
        {
            playerPos.y = -50;
        }
        transform.position = playerPos;
    }
    private IEnumerator DashTimer()
    {
        cooldownUp = false;
        t = 0;
        //Enables trail to indicate you're dashing
        trailrenderer.emitting = true;
        //Plays dash audio
        dashAudio.Play();
        //Removes damage listener so you don't take damage while dashing
        damageListener.damageEvent.RemoveListener(damageListener.Hit);
        while (t < timer)
        {
            t += Time.deltaTime;
            yield return null;
        }
        //Adds listener back after damage is over
        damageListener.damageEvent.AddListener(damageListener.Hit);
        //Disable Trail
        trailrenderer.emitting = false;
        //Set speed back to normal
        speed /= 3;
        //Stop the dash coroutine and start the cooldown coroutine
        StopCoroutine(DashTimer());
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        //Starts cooldown timer
        t2 = 0;
        text.gameObject.SetActive(true);
        while (t2 < cooldownTimer)
        {
            t2 += Time.deltaTime;
            timerCooldown -= Time.deltaTime;
            text.text = "Dash Cooldown: " + Mathf.FloorToInt(timerCooldown) + "s";
            yield return null;
        }
        //When timer is up stop the cooldown coroutine and let the player dash again with a boolean, disable text that shows you're on cooldown
        timerCooldown = 5f;
        text.gameObject.SetActive(false);
        cooldownUp = true;
        StopCoroutine(DashCooldown());
    }
}
