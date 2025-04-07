using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> spawnedZombies;
    public GameObject zombieGO;
    public Zombies zombie;
    public GameObject prefab;
    public GameObject spawner;
    public Timer timers;
    public float zombieDamage = 1;
    bool spawned = false;
    Coroutine spawnTimer;
    float timer = 10f;
    public float t;
    public float speed = 2f;
    //Health variables
    public float zombieHP = 100f, zombieMaxHP = 100f;
    public Vector3 spawnPosition;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float timerBuff = 1+(timers.time/100);
        if (!spawned)
        {
            speed = Mathf.FloorToInt(Random.Range(3, 7));
            zombieHP = Mathf.FloorToInt(Random.Range(20 * timerBuff, 300 * timerBuff));
            zombieMaxHP = zombieHP;
            zombieDamage = Mathf.FloorToInt(Random.Range(1 * timerBuff, 10 * timerBuff));
            spawnPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50,50), Random.Range(-50,50));
            zombieGO = Instantiate(prefab, spawnPosition, transform.rotation);
            zombie = zombieGO.GetComponent<Zombies>();
            zombie.zombieSpawner = this;
            spawnedZombies.Add(zombieGO);
            spawned = true;
            spawnTimer = StartCoroutine(SpawnTimer());
        }
    }
    private IEnumerator SpawnTimer()
    {
        t = 0;
        while (t < timer)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Spawned another zombie");
        spawned = false;
    }
}
