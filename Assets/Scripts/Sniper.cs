using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    // TODO: add bullet prefab and create fire point (e5)
    public GameObject bulletPrefab;
    public Transform firePoint;

    public bool disabled = false;
    public MeshRenderer mRend;
    public Color defaultColor;
    private float disableCountdown = 10f;

    [Header("Unity Setup Fields")]

    // TODO: set enemy prefab with the enemy tag
    public string enemyTag = "Enemy";

    // TODO: logic to rotate the turret when it sees an enemy (e4)
    void Start()
    {
        InvokeRepeating("UpdateClosestTarget", 0f, 0.5f);
        defaultColor = mRend.material.color;
    }

    void Update()
    {
        if (disabled == true)
        {
            disableCountdown -= Time.deltaTime;
            if (disableCountdown <= 0)
            {
                Enable();
            }
        }

        if (target == null)
        {
            return;
        }

        if (disabled != true)
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }

    void UpdateClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            Debug.Log("ENEMY:", enemy.GetComponent<Enemy>());
            Debug.Log(enemy.GetComponent<Enemy>().isTank);
        

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
            if (enemy.GetComponent<Enemy>().isTank == true)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }


        }





        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;

        }
        else
        {
            target = null;
        }
    }

    public void Disable()
    {
        disabled = true;
        mRend.material.SetColor("_Color", Color.red);
        disableCountdown = 10f;
    }

    public void Enable()
    {
        disabled = false;
        mRend.material.color = defaultColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
