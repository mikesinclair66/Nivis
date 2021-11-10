using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int damageValue = 10;
    public int totalHealth = 100;
    public int healthScaling = 5;

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
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

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
