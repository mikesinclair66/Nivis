using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHandler : MonoBehaviour
{
    public Text alertText;
    private float alertTimeStamp;

    void Update()
    {
        if (Time.time > alertTimeStamp)
        {
            alertText.text = "";
        }
    }

    public void setAlertText(string alertMessage, float alertDuration)
    {
        Debug.Log("ALERT: " + alertMessage);
        alertText.text = alertMessage;
        alertTimeStamp = Time.time + alertDuration;
    }
}
