using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;

    [Header("Attributes")]
    public string turretName = "turret";
    public float range = 15f;
    public float fireRate = 2f;
    public float fireCountdown = 0f;

    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public ArrayList activeDebuffs = new ArrayList();
    public TurretDebuff turretDebuff;
    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    void Start()
    {
        turretDebuff = GetComponent<TurretDebuff>();
    }

    void Update()
    {
        activeDebuffs = turretDebuff.returnActiveDebuffs();
    }

    public bool checkIfDebuffActive(string debuff)
    {
        if (activeDebuffs.Count > 0)
        {
            foreach (string i in activeDebuffs)
            {
                if (i == debuff)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
