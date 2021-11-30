using System;
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

    public GameObject impactEffect;
    public AudioSource impactSound;
    [HideInInspector]
    public Enemy targetEnemy;
    
    public ArrayList activeStatusEffects = new ArrayList();
    public BulletStatusEffect bulletStatusEffect;

    private void Start()
    {
        bulletStatusEffect = GetComponent<BulletStatusEffect>();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
    
    /**
     * Bullet constantly seeks target enemy. Once enemy is found, apply hit.
     */
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

    /**
     * Checks what type of bullet it is (standard or missile), then applies correct hit function.
     */
    void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound.clip, GameObject.Find("Main Camera").transform.position, impactSound.volume);
        }
        Destroy(gameObject);
    }

    /**
     * Explode and damage multiple enemies.
     */
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
    
    public bool checkIfStatusEffectActive(string statusEffect)
    {
        if (activeStatusEffects.Count > 0)
        {
            foreach (string i in activeStatusEffects)
            {
                if (i == statusEffect)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /**
     * Damage function. Will also apply status effects.
     */
    void Damage(Transform enemy)
    {
        if (impactEffect != null)
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            if (effectIns.transform.Find("Drawn Radius") != null) 
            {
                GameObject radDraw = effectIns.transform.Find("Drawn Radius").gameObject;
                radDraw.transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2, explosionRadius*2);
                Destroy(radDraw, 0.05f);                
            }
            Destroy(effectIns, 5f);
        }
        targetEnemy = enemy.GetComponent<Enemy>();
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);
            bulletStatusEffect.statusEffectActive();
        }
    }
}
