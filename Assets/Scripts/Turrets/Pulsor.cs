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

    void Start()
    {
        InvokeRepeating("pulsorCheckForEnemies", 0f, pulseCD);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
    }

    public void pulsorCheckForEnemies()
    {
        bool enemiesNear = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                enemiesNear = true;
                c.GetComponent<Enemy>().TakeDamage(damage);
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
                    c.GetComponent<Enemy>().percentHealthTaken(percentHealthDmg);
                }

                //pulsorRate upgrades
                if (pulsorRateTier >= 2)
                {
                    c.GetComponent<Debuff>().activateDoubleRSPoints();
                }
                if (pulsorRateTier >= 3 && c.GetComponent<Enemy>().enemyType != "tank")
                {
                    float pulsorRandomNumber = UnityEngine.Random.Range(0f, 100f);
                    if (pulsorRandomNumber >= (100 - instakillRNGPercentage))
                    {
                        Debug.Log("Instakill Active");
                        float instakillDamage = c.GetComponent<Enemy>().totalHealth;
                        c.GetComponent<Enemy>().TakeDamage(instakillDamage * 2);
                    }
                }
            }
        }
        if (pulseSound != null && enemiesNear)
        {
            pulseSound.Play();    
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
