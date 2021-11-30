using UnityEngine;

public class EnemyDisabler : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform target;
    public float range = 15f;
    public Transform firePoint;

    public string turretTag = "Turret";
    public Enemy enemy;

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

        else if (target.GetComponent<Turret>().checkIfDebuffActive("disabled") == false ) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Projectile bullet = bulletGO.GetComponent<Projectile>();
        if (bullet != null)
            bullet.Seek(target);
        enemy.Die();
    }

    void UpdateClosestTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject i in turrets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, i.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = i;
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
}