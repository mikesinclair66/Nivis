using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public float DelayAmount;

    private float Timer = 0f;

    void Start ()
    {
        Money = startMoney;
    }

    void Update ()
    {
        Timer += Time.deltaTime;

        if ( Timer >= DelayAmount)
        {
            Timer = 0f;
            Money += 15; // Add Money for every second or DelayAmount add this amount to money
            Debug.Log(Money); 
        }
        
    }


}
