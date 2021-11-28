using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public string enemyType = null;
    public float totalHealth;
    public float speed = 10f;
    public int damageValue = 10;

    public Debuff debuff;
    private ArrayList activeDebuffs = new ArrayList();
    public ResearchCostManager rcm;
    public int researchPointValue = 3;
    public float defaultSpeed;
    private float defaultHealth;
    public bool inRangeofRank2Pulsor;

    void Awake()
    {
        
    }
    
    void Start()
    {
        debuff = GetComponent<Debuff>();
        defaultSpeed = speed;
        defaultHealth = totalHealth;
        rcm = GameObject.Find("ResearchStation").GetComponent<ResearchCostManager>();
    }

    void Update()
    {
        if (debuff.returnActiveDebuffs().Count > 0)
        {
            activeDebuffs = debuff.returnActiveDebuffs();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            if (checkIfDebuffActive("radiated") == true)
            {
                Debug.Log("Radiation Damage Taken");
                totalHealth -= (damageAmount / debuff.radPercentage);
            }
            if (totalHealth <= 0) { Die(); }
        }
        else if (totalHealth <= 0) { Die(); }
    }

    //turret stuff
    public void percentHealthTaken(float damageAmount)
    {
        Debug.Log("Percent Damage Taken: " + (damageAmount / 100));
        if (totalHealth > 0)
        {
            totalHealth -= defaultHealth * (damageAmount / 100);
            if (totalHealth <= 0) { Die(); }
        }
        else if (totalHealth <= 0) { Die(); }
    }

    public int getKillCountValue()
    {
        return researchPointValue;
    }

    /** Used to check if specific debuff is active on enemy **/
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

    void Die()
    {
        WaveSpawner.EnemiesAlive--;

        if (checkIfDebuffActive("burning") == true)
        {
            debuff.aoeBurnOnDeath();
        }

        Destroy(gameObject);
        //Debug.Log("Kill Count: " + rcm.killCount);

        //turret stuff
        if (inRangeofRank2Pulsor == true)
        {
            rcm.killCount += getKillCountValue() * 2;
        }
        else
        {
            rcm.killCount += getKillCountValue();
        }
    }

    public float GetDefaultHealth()
    {
        return defaultHealth;
    }
}
