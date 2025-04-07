using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int xp = 0;
    public int maxXP = 100;
    public int level = 1;
    public float multiplier;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public PlayerMove playerMove;
    public BulletSpawner spawner;
    public DamageEvent damageEvent;
    public GameObject stat1, stat2, stat3;
    public GameObject currentStat;
    float randomChoice;
    float randomRarity;
    //int selected = 1;
    void Start()
    {
        stat1.GetComponentInChildren<Button>().onClick.AddListener(restore);
        stat2.GetComponentInChildren<Button>().onClick.AddListener(restore);
        stat3.GetComponentInChildren<Button>().onClick.AddListener(restore);
    }

    // Update is called once per frame
    void Update()
    {
        xpText.text = "XP: " + xp + "/" + maxXP;
        levelText.text = "Level: " + level;
        if (xp >= maxXP)
        {
            xp = xp - maxXP;
            maxXP *= Mathf.FloorToInt(maxXP*1.5f);
            level += 1;
            levelUp();
        }
    }
    public void levelUp()
    {
        stat1.SetActive(true);
        stat2.SetActive(true);
        stat3.SetActive(true);
        Time.timeScale = 0;
        for (int selected = 1; selected <= 3; selected++)
        {
            randomRarity = Random.Range(0, 100);
            if (selected == 1)
            {
                currentStat = stat1;
            }
            if (selected == 2)
            {
                currentStat = stat2;
            }
            if (selected == 3)
            {
                currentStat = stat3;
            }
            Text[] textArray = currentStat.GetComponentsInChildren<Text>();
            if (randomRarity > 90 && randomRarity < 100)
            {
                currentStat.GetComponentInChildren<Button>().onClick.AddListener(damageUp);
                textArray[0].text = "Damage Up";
                textArray[1].text = "Increases your damage by 10%";
            }
            if (randomRarity > 80 && randomRarity < 90)
            {
                currentStat.GetComponentInChildren<Button>().onClick.AddListener(fireRateUp);
                textArray[0].text = "Fire Rate Up";
                textArray[1].text = "Increases your fire rate by 10%";
            }
            if (randomRarity > 60 && randomRarity < 80)
            {
                currentStat.GetComponentInChildren<Button>().onClick.AddListener(hpUp);
                textArray[0].text = "HP Up";
                textArray[1].text = "Increases your HP by 10";
            }
            if (randomRarity > 40 && randomRarity < 60)
            {
                currentStat.GetComponentInChildren<Button>().onClick.AddListener(bulletSpeedUp);
                textArray[0].text = "Bullet Speed Up";
                textArray[1].text = "Increases the speed of your bullets by 10%";
            }
            if (randomRarity > 35 && randomRarity < 40)
            {
                if (playerMove.timer > 2)
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(healing);
                    textArray[0].text = "Healing";
                    textArray[1].text = "Heals you for 50 HP";
                }
                else
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(dashTimerUp);
                    textArray[0].text = "Dash Timer Up";
                    textArray[1].text = "Increases your duration of dash by 0.1s, can only go up to 2s";
                }
            }
            if (randomRarity > 30 && randomRarity < 35)
            {
                if (playerMove.cooldownTimer > 3)
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(dashCooldownDown);
                    textArray[0].text = "Dash Cooldown Down";
                    textArray[1].text = "Decreases the cooldown of your dash by 0.5s, can only be decreased to 3s";
                }
                else
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(healing);
                    textArray[0].text = "Healing";
                    textArray[1].text = "Heals you for 50 HP";
                }
            }
            if (randomRarity < 1)
            {
                if (!spawner.autoFire)
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(autoFire);
                    textArray[0].text = "Autofire";
                    textArray[1].text = "Allows you to fire your weapon by holding down mouse, can only be obtained once";
                }
                else
                {
                    currentStat.GetComponentInChildren<Button>().onClick.AddListener(healing);
                    textArray[0].text = "Healing";
                    textArray[1].text = "Heals you for 50 HP";
                }
            }
            if (randomRarity < 30 && randomRarity > 1)
            {
                currentStat.GetComponentInChildren<Button>().onClick.AddListener(healing);
                textArray[0].text = "Healing";
                textArray[1].text = "Heals you for 50 HP";
            }
        }
    }
    
    public void damageUp()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(damageUp);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(damageUp);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(damageUp);
        spawner.bulletDamage *= 0.1f;
    }
    public void fireRateUp()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(fireRateUp);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(fireRateUp);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(fireRateUp);
        spawner.fireRate /= 0.1f;
    }

    public void autoFire()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(autoFire);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(autoFire);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(autoFire);
        spawner.autoFire = true;
    }
    public void bulletSpeedUp()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(bulletSpeedUp);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(bulletSpeedUp);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(bulletSpeedUp);
        spawner.bulletSpeed *= 0.1f;
    }
    public void hpUp()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(hpUp);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(hpUp);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(hpUp);
        damageEvent.playerMaxHP += 10f;
        damageEvent.playerHP += 10f;
    }
    public void dashTimerUp()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(dashTimerUp);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(dashTimerUp);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(dashTimerUp);
        playerMove.timer += 0.1f;
    }
    public void dashCooldownDown()
    {
        stat1.GetComponentInChildren<Button>().onClick.RemoveListener(dashCooldownDown);
        stat2.GetComponentInChildren<Button>().onClick.RemoveListener(dashCooldownDown);
        stat3.GetComponentInChildren<Button>().onClick.RemoveListener(dashCooldownDown);
        playerMove.timer -= 0.5f;
    }
    public void healing()
    {
        currentStat.GetComponentInChildren<Button>().onClick.AddListener(healing);
        damageEvent.playerHP += 50f;
        if (damageEvent.playerHP > damageEvent.playerMaxHP)
        {
            damageEvent.playerHP = damageEvent.playerMaxHP;
        }
    }
    public void restore()
    {
        Time.timeScale = 1;
        stat1.SetActive(false);
        stat2.SetActive(false);
        stat3.SetActive(false);
    }
}
