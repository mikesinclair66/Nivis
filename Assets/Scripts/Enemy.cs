using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int damageValue = 10;
    public float totalHealth;
    //public int healthScaling = 5;

    public int radPercentage;
    public float radTimer = 3f;
    public float radDuration;
    public bool radiation;

    public float stunDuration;
    public bool stunnedUnit;
    public int slowAmount = 2;
    //public SkinnedMeshRenderer mRend;
    //public Color defaultColor;
    public bool isTank;
    public bool meleeSlowedUnit;
    public float meleeSlowTimer = 0.5f;
    public bool burning;
    public float burnTimer = 5f;
    public float TickDamageTimer = 0.5f;
    private float defaultSpeed;
    private float burnDamage;

    void Start()
    {
        //defaultColor = mRend.material.color;
        defaultSpeed = speed;
    }

    void Update()
    {
        if (radiation == true)
        {
            radTimer -= Time.deltaTime;
            if (radTimer <= 0)
            {
                radOff();
            }
        }

        if (stunnedUnit == true)
        {
            //Debug.Log("Unit Stunned");
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0)
            {
                stunOff();
            }
        }
        if (meleeSlowedUnit == true)
        {
            //Debug.Log("Unit Slowed");
            meleeSlowTimer -= Time.deltaTime;
            if (meleeSlowTimer <= 0)
            {
                meleeSlowOff();
            }
        }
        if (burning == true)
        {
            burnTimer -= Time.deltaTime;
            TakeDamage(burnDamage * Time.deltaTime);
            if (burnTimer <= 0)
            {
                BurnOff();
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            if (radiation == true)
            {
                totalHealth -= (damageAmount / radPercentage);
            }

            if (totalHealth <= 0) { Die(); }

        }
        else if (totalHealth <= 0) { Die(); }
    }

    public void meleeDamage(int radiatorDamage)
    {
        Debug.Log("Enemy Found");
        TakeDamage(radiatorDamage);
        meleeActivateSlow();
        //mRend.material.SetColor("_Color", Color.red);

    }

    public void meleeActivateSlow()
    {
        meleeSlowedUnit = true;
        speed = defaultSpeed / slowAmount;
        meleeSlowTimer = 0.5f;
        //meleeSlowOff();
    }

    public void meleeSlowOff()
    {
        meleeSlowedUnit = false;
        //mRend.material.color = defaultColor; 
        Debug.Log("DEFAULT SPEED: " + defaultSpeed);
        speed = defaultSpeed;
    }
    /**
    public void ScaleHP()
    {
        if(WaveSpawner.waveIndex > 0){totalHealth += healthScaling;}
        else{return;}
    }**/

    public void activateRad()
    {
        radiation = true;
        //mRend.material.SetColor("_Color", Color.green);
        radTimer = 3f;
    }

    public void radOff()
    {
        radiation = false;
        //mRend.material.color = defaultColor; 
    }

    public void activateBurn(float burnDmg)
    {
        burning = true;
        //StartCoroutine(BurnTick());
        burnTimer = 5f;
        burnDamage = burnDmg;

    }


    public void BurnOff()
    {
        burning = false;
    }

    // IEnumerator BurnTick(){
    //     yield return new WaitForSeconds(TickDamageTimer);
    // }


    public void activateStun()
    {
        stunnedUnit = true;
        speed = 0f;
        stunDuration = 1.5f;
        //mRend.material.SetColor("_Color", Color.blue);

    }

    public void stunOff()
    {
        stunnedUnit = false;
        speed = defaultSpeed;
        //mRend.material.color = defaultColor; 

    }

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
