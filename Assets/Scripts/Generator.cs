using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public int totalHealth = 100;

    public Text genHealth;
    public GameObject canvas;

    void Start()
    {
        Debug.Log("HP: " + totalHealth);
    }

    void Update()
    {
        genHealth.text = totalHealth + "%";
        //HealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageAmount;
            canvas.GetComponent<Hotbar>().TakeDamage(totalHealth);

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

/*  
    void HealthBar()
    {
        origPos = damageModBar.transform.position;
        origScale = damageModBar.transform.localScale;
        genHealth.text = damageMod * 100.0f + "%";
        
        if (totalHealth > 0)
        {
            damageMod = (float)totalHealth / 100.0f;
            damageModBar.transform.localScale = new Vector3(origScale.x * damageMod, origScale.y, origScale.z);
        }
    }
*/
}
