using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Set variables for the gun rotation and mouse position
        Vector3 gunRot = transform.eulerAngles;
        Vector3 mousePos = Input.mousePosition;
        //Adjusts the mouse position to account for the screen
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Accounts for the position of the object
        Vector2 dir = screenPos - transform.position;
        //Calculates the angle between your mouse and the object and converts it to degrees
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
        //turretRot.z = mousePos.y*mousePos.x*-1;
        //Set only the z value to the updated angle as its the only value we need
        gunRot.z = angle;
        transform.eulerAngles = gunRot;
        //transform.Rotate(0, 0, angleInRadians);
    }
}
