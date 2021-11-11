using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;
    GameObject generator;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        generator = GameObject.Find("END");
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        Vector3 relativePos = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        generator.GetComponent<Generator>().TakeDamage(enemy.damageValue);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
