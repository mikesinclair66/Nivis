using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public int totalHealth = 100;
    public Text totalHealthText;
    public GameObject damageModBar;
    float damageMod = 1.0f;

    Vector3 origPos, origScale;

    void Start()
    {
        Debug.Log("HP: " + totalHealth);
        origPos = new Vector3(
            damageModBar.transform.position.x,
            damageModBar.transform.position.y,
            damageModBar.transform.position.z
        );
        origScale = new Vector3(
            damageModBar.transform.localScale.x,
            damageModBar.transform.localScale.y,
            damageModBar.transform.localScale.z
        );
    }

    void Update()
    {
        totalHealthText.text = totalHealth.ToString();
        origPos = damageModBar.transform.position;
        origScale = damageModBar.transform.localScale;
    }

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            damageMod = (float)totalHealth / 100.0f;
            damageModBar.transform.localScale = new Vector3(damageMod, origScale.y, origScale.z);
            //damageModBar.transform.position = origPos + new Vector3(-(1.0f - damageMod) * (float)origScale.x, 0, 0);

            Debug.Log("HP: " + totalHealth);
            if (totalHealth <= 0)
            {
                SceneManager.LoadScene("EndScene");
            }
        }else if (totalHealth <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
    
}
