using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //Initialize GameObjects
    //Gameobject for the bullet
    public GameObject prefab;
    //Gameobject for the muzzle
    public GameObject prefab2;
    //Gameobject to determine where the muzzle spawns
    public GameObject spawner;
    //Classes for the bullet and muzzle
    public GameObject bulletGO;
    public TextMeshProUGUI damageT, bulletspeedT, firerateT;
    public Bullet bullet;
    public Muzzle muzzle;
    int counter;
    //Values to control gun stats
    public bool autoFire = false;
    public float fireRate = 100f;
    public float bulletSpeed = 50f;
    public float bulletDamage = 10f;
    public List<GameObject> spawnedBullets;
    public CinemachineImpulseSource impulseSource;
    void Start()
    {
        //Initialize list to track spawned bullets
        spawnedBullets = new List<GameObject>();
    }

    //Changes whether or not the gun is autofire or not with a button
    public void changeFire(bool f)
    {
        if (f)
        {
            autoFire = true;
            //Debug.Log(true);
        }
        if (!f)
        {
            autoFire = false;
            //Debug.Log(false);
        }
        //autoFire = f;
    }
    //Changes the fire rate with a slider
    public void changeFireRate(float fr)
    {
        fireRate = fr;
    }
    public void changeBulletSpeed(float bs)
    {
        bulletSpeed = bs;
    }
    public void changeBulletDamage(float bd)
    {
        bulletDamage = bd;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the number values in the ui to the stats of the weapons
        damageT.text = bulletDamage.ToString();
        bulletspeedT.text = bulletSpeed.ToString();
        firerateT.text = fireRate.ToString();
        //Constantly increase the counter to account for firing rate
        counter++;
        bool fire = Input.GetMouseButtonDown(0);
        if (autoFire == false)
        {
            //Get mouse button down forces you to click for it to activate
            fire = Input.GetMouseButtonDown(0);
        }
        if (autoFire == true)
        {
            //Get mouse button continously fires as long as you hold the mouse button down
            fire = Input.GetMouseButton(0);
        }
        //Prevent the gun from shooting if the cursor is over the UI
        //if (EventSystem.current.IsPointerOverGameObject()) return;
        //If the counter is over the fire rate it lets you shoot
        if (fire && counter >= fireRate)
        {
            //Spawns the bullet and muzzle flash
            bulletGO = Instantiate(prefab, transform.position, transform.rotation);
            bullet = bulletGO.GetComponent<Bullet>();
            //Set the variable in the bullet script to know that this is the script that spawned it
            bullet.spawner = this;
            //Add the spawned bullets to a list
            spawnedBullets.Add(bulletGO);
            //Set the bullet speed and damage on the bullet spawner script as I couldn't figure out a way to edit the prefab directly
            bullet.bulletSpeed(bulletSpeed);
            bullet.bulletDamage(bulletDamage);
            //Sets the position of the muzzle where the spawner is
            GameObject muzzleGO = Instantiate(prefab2, spawner.transform.position, transform.rotation);
            muzzle = muzzleGO.GetComponent<Muzzle>();
            //Set the muzzle's parent to the muzzle spawner to account for rotation
            muzzleGO.transform.parent = spawner.transform;
            //Reset counter to 0 to account for firing delay
            counter = 0;
            impulseSource.GenerateImpulse();
        }
    }
}
