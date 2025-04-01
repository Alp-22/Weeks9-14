using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Adjust player speed
        float speed = 0.005f;
        Vector2 playerPos = transform.position;
        //If W is pressed move up
        if (Input.GetKey(KeyCode.W))
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
        transform.position = playerPos;
    }
}
