using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Turret turret;
    public int sniperTier;
    void Start()
    {
        turret = GetComponent<Turret>();
        InvokeRepeating("UpdateClosestTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (turret.target == null)
        {
            return;
        }

        if (turret.checkIfDebuffActive("disabled") != true)
        {
            // Target lock on
            if (turret.partToRotate != null)
            {
                turret.partToRotate.LookAt(new Vector3(turret.target.position.x, turret.partToRotate.position.y, turret.target.position.z));
            }
            
            if (turret.fireCountdown <= 0)
            {
                Shoot();
                turret.fireCountdown = 1f / turret.fireRate;
            }

            turret.fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(turret.bulletPrefab, turret.firePoint.position, turret.firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (turret.shootSound != null)
            {
                turret.shootSound.Play();
            }
            bullet.Seek(turret.target);
        }
    }

    void UpdateClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(turret.enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            //Debug.Log("ENEMY:", enemy.GetComponent<Enemy>());
            //Debug.Log(enemy.GetComponent<Enemy>().isTank);
        

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
            if (enemy.GetComponent<Enemy>().enemyType == "tank")
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }


        }
        
        if (nearestEnemy != null && shortestDistance <= turret.range)
        {
            turret.target = nearestEnemy.transform;

        }
        else
        {
            turret.target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turret.range);
    }
}
