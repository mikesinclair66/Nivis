using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private GameObject generator;
    private int damageValue, totalHealth, healthScaling;

    void Start()
    {
        damageValue = 10;
        totalHealth = 100;
        healthScaling = 10;
        target = Waypoints.points[0];
        generator = GameObject.Find("END");
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            generator.GetComponent<Generator>().TakeDamage(damageValue);
            return;
        }

        wavepointIndex++;
        if (wavepointIndex % 2 == 0)
        {
            totalHealth = (100 + (healthScaling * (wavepointIndex - 1)));
        }
        target = Waypoints.points[wavepointIndex];
    }

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            if (totalHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (totalHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
