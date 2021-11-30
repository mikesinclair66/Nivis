using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public int totalHealth = 100;
    public int shieldHealth = 0;
    public Text genHealth;
    public GameObject canvas;
    public MeshRenderer mRend;

    void Start()
    {
        Debug.Log("HP: " + totalHealth);
    }

    void Update()
    {
        genHealth.text = totalHealth + "%";
    }

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            int exceedingDamage = 0;
            if (shieldHealth > 0)
            {
                shieldHealth -= damageAmount;
                Debug.Log("Shield: " + shieldHealth);
                if (shieldHealth < 0)
                {
                    mRend.enabled = false;
                    exceedingDamage = shieldHealth * -1;
                    shieldHealth = 0;
                }
                totalHealth -= exceedingDamage;
                canvas.GetComponent<Hotbar>().TakeDamage(totalHealth);

                Debug.Log("HP: " + totalHealth + " Shield: " + shieldHealth);
                if (totalHealth <= 0)
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
            else
            {
                totalHealth -= damageAmount;
                canvas.GetComponent<Hotbar>().TakeDamage(totalHealth);

                if (totalHealth <= 0)
                {
                    SceneManager.LoadScene("EndScene");
                }
            } 
        }
        else if (totalHealth <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }


}
