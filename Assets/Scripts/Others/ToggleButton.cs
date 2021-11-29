using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ToggleButton is switched between two images when toggled
/// off and on.
/// </summary>
public class ToggleButton : MonoBehaviour
{
    bool toggled = false;
    public GameObject toggledOff, toggledOn;//the two inner images

    void Start()
    {
        toggledOn.SetActive(false);
    }

    public void Toggle()
    {
        toggled = !toggled;
        toggledOff.SetActive(!toggled);
        toggledOn.SetActive(toggled);
    }
}
