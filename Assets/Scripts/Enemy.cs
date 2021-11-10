using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed;
    private float defaultSpeed;
    public int damageValue = 10;
    public int totalHealth;
    public int healthScaling = 5;
    public bool radiation;
    public int radPercentage;
    public float radDuration;
    public float stunDuration = 2f;
    public bool stunnedUnit;
    public MeshRenderer mRend;
    public Color defaultColor;
    public float radTimer = 3f;
    public bool isTank;


    void Start()
    {
        defaultColor = mRend.material.color;
        defaultSpeed = speed;

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
        if (stunnedUnit == true){
            Debug.Log("Unit Stunned");
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0){
                stunOff();
            }
        }
    }
    
    public void TakeDamage(int damageAmount)
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
        mRend.material.SetColor("_Color", Color.green);
        radTimer = 3f; 
    }

    public void radOff()
    {
        radiation = false;
        mRend.material.color = defaultColor; 
    }

    public void activateStun()
    {
        stunnedUnit = true;
        speed = 0f;
        stunDuration = 1.5f;
        mRend.material.SetColor("_Color", Color.blue);

    }

    public void stunOff(){
        stunnedUnit = false;
        speed = defaultSpeed;
        mRend.material.color = defaultColor; 

    }

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
