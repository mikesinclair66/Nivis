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

    // TODO: add bullet prefab and create fire point (e5)
    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool disabled = false;

    public Renderer turretRenderer;
    
    [Header("Unity Setup Fields")]

    // TODO: set enemy prefab with the enemy tag
    public string enemyTag = "Enemy";
    
    // TODO: logic to rotate the turret when it sees an enemy (e4)
    void Start()
    {
        InvokeRepeating("UpdateClosestTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (disabled = false) 
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
        turretRenderer.material.SetColor("_Color", Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
