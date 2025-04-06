using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> spawnedZombies;
    public GameObject zombieGO;
    public Zombies zombie;
    public GameObject prefab;
    public GameObject spawner;
    public int zombieDamage = 1;
    bool spawned = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            zombieGO = Instantiate(prefab, transform.position, transform.rotation);
            zombie = zombieGO.GetComponent<Zombies>();
            zombie.zombieSpawner = this;
            spawnedZombies.Add(zombieGO);
            spawned = true;
        }
    }
}
