using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsor : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public string turretName = "pulsor";
    public float range;
    public float damage;
    public float pulseCD;
    public Transform firePoint;

    public int pulsorRangeTier;
    public int pulsorRateTier;

    public AudioSource pulseSound;

    [Header("Range Upgrade Attributes")]
    public Drill drill;
    public int moneyEarned = 5;
    public int moneyRNGPercentage;
    public float percentHealthDmg;

    [Header("Rate Upgrade Attributes")]
    public float instakillRNGPercentage = 10f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    /**
     * Start function activates the pulsor ability, with a cooldown of pulseCD for when it should activate.
     */
    void Start()
    {
        InvokeRepeating("pulsorCheckForEnemies", 0f, pulseCD);
    }

    /**
     * If no enemies are in range, do nothing.
     */
    void Update()
    {
        if (target == null)
        {
            return;
        }
    }

    /**
     * This function checks for all enemies in range of the pulsor AoE.
     * It will also check for the tiers and which upgrade path was chosen for the pulsor to activate certain debuffs / effects.
     */
    public void pulsorCheckForEnemies()
    {
        bool enemiesNear = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                enemiesNear = true;
                if (c.GetComponent<Enemy>().totalHealth > 0)
                {
                    c.GetComponent<Enemy>().TakeDamage(damage);
                }
                c.GetComponent<Debuff>().activateSlow();

                //pulsorRange upgrades
                if (pulsorRangeTier >= 2)
                {
                    float randomNumber = UnityEngine.Random.Range(0f, 100f);
                    if (randomNumber >= (100 - moneyRNGPercentage))
                    {
                        Debug.Log("Gave Currency");
                        drill.currentMoney += moneyEarned;
                    }
                }
                if (pulsorRangeTier >= 3)
                {
                    if (c.GetComponent<Enemy>().totalHealth > 0)
                    {
                        c.GetComponent<Debuff>().activatePercentHP();
                    }
                }

                //pulsorRate upgrades
                if (pulsorRateTier >= 2)
                {
                    c.GetComponent<Debuff>().activateDoubleRSPoints();
                }
                if (pulsorRateTier >= 3 &&
                    (c.GetComponent<Enemy>().enemyType != "tank" && c.GetComponent<Enemy>().enemyType != "boss"))
                {
                    float pulsorRandomNumber = UnityEngine.Random.Range(0f, 100f);
                    if (pulsorRandomNumber >= (100 - instakillRNGPercentage))
                    {
                        Debug.Log("Instakill Active");
                        float instakillDamage = c.GetComponent<Enemy>().totalHealth;
                        if (c.GetComponent<Enemy>().totalHealth > 0)
                        {
                            c.GetComponent<Enemy>().TakeDamage(instakillDamage * 2);
                        }
                    }
                }
            }
        }
        if (pulseSound != null && enemiesNear)
        {
            pulseSound.Play();
        }
    }
    
    /**
     * Event function to draw range while in scene. Used for debugging.
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
