using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    //public float range = 15f;
    //private float fireCountdown = 0f;

    // TODO: add bullet prefab and create fire point (e5)
    //public GameObject bulletPrefab;
    public Transform firePoint;

    //public bool disabled = false;
    public MeshRenderer mRend;
    public Color defaultColor;
    //private float disableCountdown = 10f;
    public bool MeleeTurret;
    public float MeleeRange;
    public int meleeDamage;
    public float fireRate;

    [Header("Unity Setup Fields")]

    // TODO: set enemy prefab with the enemy tag
    public string enemyTag = "Enemy";

    // TODO: logic to rotate the turret when it sees an enemy (e4)
    void Start()
    {
        InvokeRepeating("UpdateClosestTarget", 0f, fireRate);
        defaultColor = mRend.material.color;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
    }
    void UpdateClosestTarget()
    {
        MeleeCheckForEnemies();
    }

    public void MeleeCheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, MeleeRange);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                c.GetComponent<Enemy>().meleeDamage(meleeDamage);
            }
        }
    }
    // public void Disable()
    // {
    //     disabled = true;
    //     mRend.material.SetColor("_Color", Color.red);
    //     disableCountdown = 10f;
    // }

    // public void Enable()
    // {
    //     disabled = false;
    //     mRend.material.color = defaultColor;
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeRange);
    }
}
