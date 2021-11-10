using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;
    public int damage = 50;
    public float explosionRadius = 0f;
    //public int dotDamage = 0;
    public bool radiation;
    public bool stunShot;

    // TODO: add particle effect (e5)

    public void Seek(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    // TODO: find which video this code is relevant, might remove before alpha build
    void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        // else if (stunShot == true && sniperUnit == true)
        // {
        //     Sniper();
        // }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
                
            }
        }
    }

    // void Sniper()
    // {
    //     if (isTank == true)
    //     {
    //         Damage();
    //     }
    //     else {
    //         Damage(target);
    //     }
    // }

    

    // TODO: connect with enemy code
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
            Debug.Log("Reach here");
            if (radiation == true)
            {
                e.activateRad();
            }
            if (stunShot == true)
            {
                e.activateStun();
                Debug.Log("Activate Stun: " + e.stunnedUnit );
            }
        }
        
    }
}
