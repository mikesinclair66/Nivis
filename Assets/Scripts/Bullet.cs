using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public bool burnShot;
    public float burnDamage = 5f;
    public bool rank3Burn;

    public GameObject impactEffect;
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
        if (impactEffect != null)
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            if (effectIns.transform.Find("Drawn Radius") != null) {
                GameObject radDraw = effectIns.transform.Find("Drawn Radius").gameObject;
                radDraw.transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2, explosionRadius*2);
                Destroy(radDraw, 0.05f);                
            }
            Destroy(effectIns, 5f);
        }
        
        Enemy e = enemy.GetComponent<Enemy>();
        Debuff debuff = enemy.GetComponent<Debuff>();

        if (e != null)
        {
            e.TakeDamage(damage);
            //Debug.Log("Reach here");
            if (radiation == true)
            {
                debuff.activateRad();
            }
            if (stunShot == true)
            {
                debuff.activateStun();
                //Debug.Log("Activate Stun: " + e.stunnedUnit );
            }
            if (burnShot == true){
                if(debuff.frozen == true) {
                    Debug.Log("Melt Damage Reached");
                    debuff.freezeOff();
                    e.TakeDamage(damage * 2);
                }
                debuff.activateBurn(burnDamage, rank3Burn);
            }
        }
        
    }
}
