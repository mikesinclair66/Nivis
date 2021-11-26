using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    //public float range = 15f;
    //private float fireCountdown = 0f;

    // TODO: add bullet prefab and create fire point (e5)
    //public GameObject bulletPrefab;
    public Transform firePoint;

    //public bool disabled = false;
    public MeshRenderer mRend;
    public Color defaultColor;
    //private float disableCountdown = 10f;
    public bool pulsorTurret;
    public float pulsorRange;
    public float pulsorDamage;
    public float PulseCD;
    public Drill drill;
    public int moneyEarned = 5;
    public int rngPercentage;
    public bool rank3Pulsor;
    public float percentHealthDmg;
    public bool rank2Pulsor;
    public bool instakillPulsor;
    public float pulsorRNGPercentage = 10f;

    [Header("Unity Setup Fields")]

    // TODO: set enemy prefab with the enemy tag
    public string enemyTag = "Enemy";

    // TODO: logic to rotate the turret when it sees an enemy (e4)
    void Start()
    {
        InvokeRepeating("pulsorCheckForEnemies", 0f, PulseCD);
        // defaultColor = mRend.material.color;
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

        Collider[] colliders = Physics.OverlapSphere(transform.position, pulsorRange);
        foreach (Collider c in colliders)
        {


            if (c.GetComponent<Enemy>())
            {
                c.GetComponent<Enemy>().TakeDamage(pulsorDamage);
                c.GetComponent<Debuff>().activateSlow();
                if (rank2Pulsor == true)
                {
                    c.GetComponent<Enemy>().inRangeofRank2Pulsor = true;
                }
                float randomNumber = UnityEngine.Random.Range(0f, 100f);
                //Debug.Log("RANDOM VALUE: " + randomNumber);
                //Debug.Log("Rng Percent: " + (100 - rngPercentage));
                if (randomNumber >= (100 - rngPercentage))
                {
                    Debug.Log("Gave Currency");
                    drill.currentMoney += moneyEarned;

                }
                if (rank3Pulsor == true)
                {
                    c.GetComponent<Enemy>().percentHealthTaken(percentHealthDmg);

                }
                if (instakillPulsor == true && c.GetComponent<Enemy>().enemyType == "tank")
                {
                    float pulsorRandomNumber = UnityEngine.Random.Range(0f, 100f);
                    if (pulsorRandomNumber >= (100 - pulsorRNGPercentage))
                    {
                        Debug.Log("Instakill Active");
                        float instakillDamage = c.GetComponent<Enemy>().totalHealth;
                        c.GetComponent<Enemy>().TakeDamage(instakillDamage * 2);
                    }

                }


            }
        }
    }
    // public void Disable()
    // {
    //     disabled = true;
    //     mRend.material.SetColor("_Color", Color.red);
    //     disableCountdown = 10f;
    // }

    // public void Enable()
    // {
    //     disabled = false;
    //     mRend.material.color = defaultColor;
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pulsorRange);
    }
}
