using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{

    public Turret turret;

    [Header("Attributes")]

    public LineRenderer lineRenderer;
    public int damageOverTime = 10;
    public int LaserTier;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    /**
     * Constantly check for closest target
     */
    void Start()
    {
        turret = GetComponent<Turret>();
        InvokeRepeating("UpdateClosestTarget", 0f, 0.5f);
    }

    /**
     * Constantly look around for an enemy and shoot at it if the turret isn't disabled.
     */
    void Update()
    {
        if (turret.target == null)
        {
            if (turret.shootSound != null)
            {
                turret.shootSound.Stop();
            }
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }

        if (turret.checkIfDebuffActive("disabled") != true)
        {
            // Target lock on
            if (turret.partToRotate != null)
            {
                turret.partToRotate.LookAt(new Vector3(turret.target.position.x, turret.partToRotate.position.y, turret.target.position.z));
            }
            Laser();
        }
        else
        {
            turret.shootSound.Stop();
            lineRenderer.enabled = false;
        }
    }

    /**
     * Instantiate a line renderer and point at target enemy.
     */
    void Laser()
    {
        if (turret.shootSound != null && !turret.shootSound.isPlaying)
        {
            turret.shootSound.Play();
        }
        if (turret.target.GetComponent<Enemy>().totalHealth > 0)
        {
            turret.target.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime * turret.fireRate);
        }
        if (LaserTier == 3){

            turret.target.GetComponent<Debuff>().ChanceToFreeze();
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, turret.firePoint.position);
        lineRenderer.SetPosition(1, turret.target.position);
    }

    /**
     * Find the closest target.
     */
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

