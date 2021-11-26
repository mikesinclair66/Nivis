using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Debuff : MonoBehaviour
{
    public Enemy enemy;
    private ArrayList activeDebuffs = new ArrayList();

    [Header("Burn")]

    public bool burning;
    public bool rank3Burn;
    public float burnRadius = 15f;
    public float burnTimer = 5f;
    private float burnDamage;

    [Header("Radiation")]

    public bool radiation;
    public int radPercentage = 25;
    public float radTimer = 3f;
    public float radDuration;

    [Header("Stun")]
    public float stunDuration;
    public bool stunned;

    [Header("Slow")]
    public bool slowed;
    public float slowTimer = 0.5f;
    public int slowAmount = 2;
    public bool inRangeofRank2Melee;

    [Header("Freeze")]
    public float freezeChance = 0.25f;
    public float freezeDuration = 1.5f;
    public bool frozen;
    public float freezeTimer = 1.5f;
    public float meltDamage = 300f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //check burning timer
        if (burning == true)
        {
            burnTimer -= Time.deltaTime;
            enemy.TakeDamage(burnDamage * Time.deltaTime);
            if (burnTimer <= 0)
            {
                burning = false;
                activeDebuffs.Remove("burning");
            }
        }

        //check radiation timer
        if (radiation == true)
        {
            radTimer -= Time.deltaTime;
            if (radTimer <= 0)
            {
                radiation = false;
                activeDebuffs.Remove("radiated");
            }
        }

        //check stun timer
        if (stunned == true)
        {
            //Debug.Log("Unit Stunned AGAIN");
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0)
            {
                stunned = false;
                Debug.Log("Stun Removed");
                activeDebuffs.Remove("stunned");
                enemy.speed = enemy.defaultSpeed;
            }
        }

        //check freeze timer
        if (slowed == true)
        {
            //Debug.Log("Unit Slowed");
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                //Debug.Log("Deactivate Slow");
                slowed = false;                
                activeDebuffs.Remove("slowed");
                enemy.speed = enemy.defaultSpeed;
                inRangeofRank2Melee = false;
            }
        }

        if (frozen == true)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                Debug.Log("Timer hit 0" + freezeTimer);
                freezeOff();
            }
        }
    }

    /** returns arraylist of all active debuffs **/
    public ArrayList returnActiveDebuffs()
    {
        return activeDebuffs;
    }

    /*********************** Burn *************************/

    /** Activates the Burn Damage Debuff **/
    public void activateBurn(float burnDmg, bool rank3Burn)
    {
        //Debug.Log("Burn Activated");
        burning = true;
        activeDebuffs.Add("burning");
        if (rank3Burn == true)
        {
            //Debug.Log("RANK 3 BURN ON");
            //Debug.Log("AOEBURN ACTIVE: " + aoeBurn);
            this.rank3Burn = rank3Burn;
        }
        burnTimer = 5f;
        burnDamage = burnDmg;
    }

    /** Uses a collider to check for enemies inside the range
        of the Burn on Death Effect **/
    public void aoeBurnDmg(float burnDmg)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, burnRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                //Debug.Log("AOE BURN DMG SPREAD AND ACTIVE");
                activateBurn(burnDmg, true);
            }
        }
    }
    /** Burn on Death Effect activated if
        the rank 3 fire bullet attacks the enemy **/
    public void aoeBurnOnDeath()
    {
        if (rank3Burn == true)
        {
            //Debug.Log("Burn ON DEATH");
            aoeBurnDmg(burnDamage);
        }
    }

    /*********************** Radiation *************************/

    /** Activate Radiation Debuff on Enemy **/
    public void activateRad()
    {
        //Debug.Log("Radiation Active");
        radiation = true;
        activeDebuffs.Add("radiated");
        radTimer = 3f;
    }
    /*********************** Stun *************************/

    /** Activate Stun Effect on Enemy **/
    public void activateStun()
    {
        //Debug.Log("Unit Stunned");
        stunned = true;
        activeDebuffs.Add("stunned");
        enemy.speed = 0f;
        stunDuration = 1.5f;
    }
    /*********************** Slow *************************/
    /** Activate Slow Effect on Enemy **/
    public void activateSlow()
    {
        //Debug.Log("Unit Slowed");
        slowed = true;
        activeDebuffs.Add("slowed");
        enemy.speed = enemy.defaultSpeed / slowAmount;
        slowTimer = 0.5f;
    }

    /*********************** Freeze *************************/
    public void ChanceToFreeze()
    {
        //Debug.Log("Freeze Reach Here");
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        //Debug.Log("Ran Num: " + randomNumber);
        //Debug.Log("Freeze Percent: " + freezeChance);

        if (randomNumber >= (100 - freezeChance))
        {
            //Debug.Log("Freeze chance Succeeded");
            activateFreeze();
        }
    }

    /** Activate Freeze Effect on Enemy **/
    public void activateFreeze()
    {
        frozen = true;
        activeDebuffs.Add("frozen");
        enemy.speed = 0f;
        freezeTimer = 1.5f;
        //Debug.Log("Frozen Unit: " + frozen);
    }

    public void freezeOff()
    {
        //Debug.Log("Freeze Deactivated");
        freezeTimer = 0f;
        activeDebuffs.Remove("frozen");
        enemy.speed = enemy.defaultSpeed;
        frozen = false;
    }
}


