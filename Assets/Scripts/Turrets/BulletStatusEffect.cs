using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatusEffect : MonoBehaviour
{
    public bool radiation;
    public bool stunShot;
    public bool burnShot;
    public float burnDamage = 5f;
    public bool deathBurn;

    public ArrayList activeStatusEffects = new ArrayList();
    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Bullet>();
    }

    public void statusEffectActive()
    {
        Debuff debuff = bullet.targetEnemy.GetComponent<Debuff>();
        if (radiation == true)
        {
            debuff.activateRad();
        }
        if (stunShot == true)
        {

            debuff.ChanceToStun();
        }
        if (burnShot == true)
        {
            if(debuff.frozen == true) 
            {
                Debug.Log("Melt Damage Reached");
                debuff.freezeOff();
                if (bullet.targetEnemy.totalHealth > 0)
                {
                    bullet.targetEnemy.TakeDamage(bullet.damage * 2);
                }   
            }
            debuff.activateBurn(burnDamage, deathBurn);
        }
    }

    public ArrayList returnActiveStatusEffects()
    {
        return activeStatusEffects;
    }
    
}
