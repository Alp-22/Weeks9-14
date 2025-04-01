using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSprite : MonoBehaviour
{
    // Start is called before the first frame update
    //Variables to get sprite values
    int spriteValue = 0;
    //public GameObject bulletSpawner;
    //public GameObject muzzleSpawner;
    public Image image;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public GameObject bulletSpawner;
    public BulletSpawner spawner;
    void Start()
    {
        
    }
    //Gets the dropdown value from the UI
    public void spriteChange(int sprite)
    {
        spriteValue = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        spawner = bulletSpawner.GetComponent<BulletSpawner>();
        if (spriteValue == 0)
        {
           //transform.localScale = new Vector3(0.567f, 0.567f, 0.567f);
            //bulletSpawner.transform.position = new Vector3(1.58f, 0.68f, 0);
            //muzzleSpawner.transform.position = new Vector3(1.82f, -0.13f, 0);

            //Set the gun sprite and image sprite in UI to the pistol
            spriteRenderer.sprite = sprite1;
            image.sprite = sprite1;
            //Change the stats of the gun to pistol stats
            /*spawner.autoFire = false;
            spawner.bulletDamage = 10f;
            spawner.bulletSpeed = 50f;
            spawner.fireRate = 100f;*/
        }
        if (spriteValue == 1)
        {
            //transform.localScale = Vector3.one;
            //bulletSpawner.transform.position = new Vector3(2.42f, 0.23f, 0);

            //Set the gun sprite and image sprite in UI to the assault rifle
            spriteRenderer.sprite = sprite2;
            image.sprite = sprite2;
            //Change the stats of the gun to assault rifle stats
            /*spawner.autoFire = true;
            spawner.bulletDamage = 20f;
            spawner.bulletSpeed = 80f;
            spawner.fireRate = 200f;*/
        }
        if (spriteValue == 2)
        {

            //Set the gun sprite and image sprite in UI to the sniper rifle
            spriteRenderer.sprite = sprite3;
            image.sprite = sprite3;
            //Change the stats of the gun to sniper rifle stats
            /*spawner.autoFire = false;
            spawner.bulletDamage = 100f;
            spawner.bulletSpeed = 100f;
            spawner.fireRate = 20f;*/
        }
    }
}
