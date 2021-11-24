using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public static bool autoStart = true;
    public static bool fastForwardOn = false;
    private float fixedDeltaTime;

    public Text fastForwardText;
    public Text autoStartText;

    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void FastForward()
    {
        if (fastForwardOn == false)
        {
            Time.timeScale = 2.0f;
            fastForwardOn = true;
            Debug.Log("if fastForwardOn: " + fastForwardOn);
            fastForwardText.text = ">>";
        }
        else
        {
            Time.timeScale = 1.0f;
            fastForwardOn = false;
            Debug.Log("else fastForwardOn: " + fastForwardOn);
            fastForwardText.text = ">";
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void Pause()
    {
        if (autoStart == false)
        {
            Time.timeScale = 0.0f;
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void Unpause()
    {
        if (autoStart == true)
        {
            Debug.Log("fastForwardOn: " + fastForwardOn);
            Time.timeScale = fastForwardOn == true ? 2.0f : 1.0f;
            Debug.Log("Unpaused");
        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void ToggleAutoStart()
    {
        if (autoStart == false)
        {
            autoStart = true;
            Unpause();
            autoStartText.text = "Pause on Wave Start On";
        }
        else
        {
            autoStart = false;
            autoStartText.text = "Pause on Wave Start Off";
        }
    }
}
