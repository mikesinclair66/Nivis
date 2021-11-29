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
        }
        else
        {
            Time.timeScale = 1.0f;
            fastForwardOn = false;
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
        Time.timeScale = fastForwardOn == true ? 2.0f : 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void ToggleAutoStart()
    {
        autoStart = !autoStart;
        if (autoStart)
            Unpause();
    }
}
