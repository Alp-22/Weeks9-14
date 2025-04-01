using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //Set the travel speed of the bullet
    float speed = 50f;
    //Set the damage of the bullet
    float damage = 10f;
    public BulletSpawner spawner;
    int counter;
    void Start()
    {

    }
    //Adjust bullet damage based on a slider, value initalized on the bullet spawner script
    public void bulletDamage(float dm)
    {
        damage = dm;
    }
    //Adjust the bullet speed based on a slider, value initalized on the bullet spawner script
    public void bulletSpeed(float sp)
    {
        speed = sp;
    }
    // Update is called once per frame
    void Update()
    {
        //Bullet travels right of where the gun is pointing
        Vector2 objectPos = transform.position;
        transform.position += transform.right * speed * Time.deltaTime;
        counter++;
        //If the bullet exists for over 480 frames it gets destroyed
        if (counter >= 10000/speed)
        {
            Destroy(gameObject);
        }
    }
}
