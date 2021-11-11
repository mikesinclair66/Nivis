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
    //public SkinnedMeshRenderer mRend;
    //public Color defaultColor;

    void Start()
    {
        //defaultColor = mRend.material.color;
    }

    void Update()
    {
        if (radiation == true){
            radTimer -= Time.deltaTime;
            if (radTimer <= 0)
            {
                radOff();
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
            
            if (totalHealth <= 0){Die();}
            
        }
        else if (totalHealth <= 0){Die();}
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

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
