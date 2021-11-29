using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebuffer : MonoBehaviour
{
    public Enemy enemy;
    public Debuff debuff;
    ArrayList activeDebuffs = new ArrayList();
    ArrayList debuffQueue = new ArrayList();

    public EnemyUI enemyUI;
    float defaultHealth, totalHealth;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        debuff = GetComponent<Debuff>();
        enemyUI.DeactivateAllDebuffs();
        enemyUI.SetVisible(false);
        defaultHealth = enemy.GetDefaultHealth();
        totalHealth = enemy.totalHealth;
        //Debug.Log(totalHealth);
    }

    void Update()
    {
        if(enemy.totalHealth != totalHealth)
        {
            totalHealth = enemy.totalHealth;
            if (!enemyUI.IsVisible())
                enemyUI.SetVisible(true);
            enemyUI.SetHealth(totalHealth, defaultHealth);
        }
        activeDebuffs = debuff.returnActiveDebuffs();
        CheckDebuffQueue();
    }

    private void CheckDebuffQueue()
    {
        // foreach(string prevDebuff in debuffQueue)
        // {
        //     if (!activeDebuffs.Contains(prevDebuff))
        //     {
        //         //debuffQueue.Remove(prevDebuff);
        //         enemyUI.SetDebuffActive(prevDebuff, false);
        //     }
        // }

        foreach(string debuff in activeDebuffs)
        {
            if (!debuffQueue.Contains(debuff))
            {
                debuffQueue.Add(debuff);
                enemyUI.SetDebuffActive(debuff, true);
            }
        }


    }
}