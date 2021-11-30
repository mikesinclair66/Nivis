using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public Enemy enemy;
    private ArrayList activeDebuffs = new ArrayList();
    private ArrayList allDebuffs = new ArrayList();

    //Constants
    public string debuffBurning = "burning",
                    debuffRadiated = "radiated",
                    debuffStunned = "stunned",
                    debuffSlowed = "slowed",
                    debuffFrozen = "frozen",
                    debuffDoubleRSPoints = "doubleRSPoints",
                    debuffPercentHP = "percentHP";

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
    public bool inRangeofRank2Pulsor;

    [Header("Freeze")]
    public float freezeChance = 0.25f;
    public float freezeDuration = 1.5f;
    public bool frozen;
    public float freezeTimer = 1.5f;
    public float meltDamage = 300f;
    public AudioSource freezeSound;

    [Header("Double Resource Points")]
    public bool doubleRSPoints;
    public float doubleRSPTimer = 0.5f;

    [Header("Percent Health Damage")]
    public bool percentHP;
    public float percentHPTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        instantiateAllDebuffs();
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
                activeDebuffs.Remove(debuffBurning);
            }
        }

        //check radiation timer
        if (radiation == true)
        {
            radTimer -= Time.deltaTime;
            if (radTimer <= 0)
            {
                radiation = false;
                activeDebuffs.Remove(debuffRadiated);
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
                activeDebuffs.Remove(debuffStunned);
                enemy.speed = enemy.defaultSpeed;
            }
        }

        //check slow timer
        if (slowed == true)
        {
            //Debug.Log("Unit Slowed");
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                //Debug.Log("Deactivate Slow");
                slowed = false;
                activeDebuffs.Remove(debuffSlowed);
                enemy.speed = enemy.defaultSpeed;
                inRangeofRank2Pulsor = false;
            }
        }

        //check freeze timer
        if (frozen == true)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                freezeOff();
            }
        }

        //check doubleRSPoints timer
        if (doubleRSPoints == true)
        {
            doubleRSPTimer -= Time.deltaTime;
            if (doubleRSPTimer <= 0)
            {
                doubleRSPoints = false;
                activeDebuffs.Remove(debuffDoubleRSPoints);
                doubleRSPTimer = 0f;
            }
        }

        //check percentHP timer
        if (percentHP == true)
        {
            percentHPTimer -= Time.deltaTime;
            if (percentHPTimer <= 0)
            {
                Debug.Log("Timer hit 0 " + percentHPTimer);
                percentHP = false;
                activeDebuffs.Remove(debuffPercentHP);
                percentHPTimer = 0f;
            }
        }

    }//end of Update

    /** Adds all available debuffs **/
    public void instantiateAllDebuffs()
    {
        allDebuffs.Add(debuffBurning);
        allDebuffs.Add(debuffRadiated);
        allDebuffs.Add(debuffStunned);
        allDebuffs.Add(debuffSlowed);
        allDebuffs.Add(debuffFrozen);
        allDebuffs.Add(debuffDoubleRSPoints);
    }

    /** returns arraylist of all debuffs **/
    public ArrayList returnAllDebuffs()
    {
        return allDebuffs;
    }

    /** returns arraylist of all active debuffs **/
    public ArrayList returnActiveDebuffs()
    {
        return activeDebuffs;
    }

    public bool hasDebuff(string debuff)
    {
        return activeDebuffs.Contains(debuff);
    }

    /*********************** Burn *************************/
    /** Activates the Burn Damage Debuff **/
    public void activateBurn(float burnDmg, bool rank3Burn)
    {
        //Debug.Log("Burn Activated");
        burning = true;
        if (!hasDebuff(debuffBurning))
        {
            activeDebuffs.Add(debuffBurning);
        }
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
     *  of the Burn on Death Effect **/
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

    /** Burn on Death Effect activated if the
     *  rank 3 fire bullet attacks the enemy **/
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
        if (!hasDebuff(debuffRadiated))
        {
            activeDebuffs.Add(debuffRadiated);
        }
        radTimer = 3f;
    }

    /*********************** Stun *************************/
    /** Activate Stun Effect on Enemy **/
    public void activateStun()
    {
        //Debug.Log("Unit Stunned");
        stunned = true;
        if (!hasDebuff(debuffStunned))
        {
            activeDebuffs.Add(debuffStunned);
        }
        enemy.speed = 0f;
        stunDuration = 1.5f;
    }

    /*********************** Slow *************************/
    /** Activate Slow Effect on Enemy **/
    public void activateSlow()
    {
        slowed = true;
        if (!hasDebuff(debuffSlowed))
        {
            activeDebuffs.Add(debuffSlowed);
        }
        enemy.speed = enemy.defaultSpeed / slowAmount;
        slowTimer = 0.5f;
    }

    /*********************** Freeze *************************/
    /** Chance to freeze Enemy using RNG **/
    public void ChanceToFreeze()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        if (randomNumber >= (100 - freezeChance))
        {
            activateFreeze();
        }
    }

    /** Activate Freeze Effect on Enemy **/
    public void activateFreeze()
    {
        frozen = true;
        if (freezeSound != null)
        {
            freezeSound.Play();
        }
        if (!hasDebuff(debuffFrozen))
        {
            activeDebuffs.Add(debuffFrozen);
        }
        enemy.speed = 0f;
        freezeTimer = 1.5f;
    }

    /** Deactivate Freeze Effect on Enemy **/
    public void freezeOff()
    {
        freezeTimer = 0f;
        activeDebuffs.Remove(frozen);
        enemy.speed = enemy.defaultSpeed;
        frozen = false;
    }

    /*********************** Double Resource Station Points *************************/
    /** Activate Double Resource Station Points Effect on Enemy **/
    public void activateDoubleRSPoints()
    {
        doubleRSPoints = true;
        if (!hasDebuff(debuffDoubleRSPoints))
        {
            doubleRSPTimer = 0.5f;
            activeDebuffs.Add(debuffDoubleRSPoints);
        }
    }

    /*********************** Percent Health Damage *************************/
    /** Activate Percent Health Damage Effect on Enemy **/
    public void activatePercentHP()
    {
        percentHP = true;
        if (!hasDebuff(debuffPercentHP))
        {
            percentHPTimer = 0.5f;
            activeDebuffs.Add(debuffPercentHP);
        }
    }
}


