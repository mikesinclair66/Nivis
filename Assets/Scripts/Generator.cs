using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public int totalHealth = 100;
    public Text totalHealthText;

    void Start()
    {
        Debug.Log("HP: " + totalHealth);
    }

    void Update()
    {
        totalHealthText.text = totalHealth.ToString();
    }

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            Debug.Log("HP: " + totalHealth);
            if (totalHealth <= 0)
            {
                Debug.Log("You Lose!");
                SceneManager.LoadScene("GameOver");
            }
        }else if (totalHealth <= 0)
        {
            Debug.Log("You Lose!");
            SceneManager.LoadScene("GameOver");
        }
    }
    
}
