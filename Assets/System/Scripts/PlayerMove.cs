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
        trailrenderer = GetComponent<TrailRenderer>();
    }

    float timer = 1f, cooldownTimer = 5f;
    public float t, t2;
    bool timerUp = false;
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


        float direction = Input.GetAxis("Horizontal");
        float direction2 = Input.GetAxis("Vertical");


        transform.position += transform.right * direction * speed * Time.deltaTime;
        transform.position += transform.up * direction2 * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownUp)
        {
            speed *= 3;
            StartCoroutine(DashTimer());
        }
    }
    private IEnumerator DashTimer()
    {
        cooldownUp = false;
        t = 0;
        trailrenderer.enabled = true;
        dashAudio.Play();
        damageListener.damageEvent.RemoveListener(damageListener.Hit);
        while (t < timer)
        {
            t += Time.deltaTime;
            yield return null;
        }
        damageListener.damageEvent.AddListener(damageListener.Hit);
        trailrenderer.enabled = false;
        speed /= 3;
        StopCoroutine(DashTimer());
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        t2 = 0;
        text.gameObject.SetActive(true);
        while (t2 < cooldownTimer)
        {
            t2 += Time.deltaTime;
            timerCooldown -= Time.deltaTime;
            text.text = "Dash Cooldown: " + Mathf.FloorToInt(timerCooldown) + "s";
            yield return null;
        }
        timerCooldown = 5f;
        text.gameObject.SetActive(false);
        cooldownUp = true;
        StopCoroutine(DashCooldown());
    }
}
