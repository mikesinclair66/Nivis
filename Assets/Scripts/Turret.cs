using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    public Transform partToRotate;
    public float turnSpeed = 10f;
    
    public GameObject bulletPrefab;
    public Transform firePoint;

    public bool disabled = false;
    public MeshRenderer mRend;
    public Color defaultColor;
    private float disableCountdown = 10f;
    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    void Start()
    {
        InvokeRepeating("UpdateClosestTarget", 0f, 0.5f);
        // defaultColor = mRend.material.color;
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
            // Target lock on
            if (partToRotate != null)
            {
                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
            }

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
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
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
        // mRend.material.SetColor("_Color", Color.red);
        disableCountdown = 10f;
    }

    public void Enable()
    {
        disabled = false;
        // mRend.material.color = defaultColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
