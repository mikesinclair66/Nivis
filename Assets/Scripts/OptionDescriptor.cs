using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionDescriptor : MonoBehaviour
{
    public GameObject descriptor, arrowUp, arrowDown;
    public Text innerText;
    string val;
    bool directionUp = true;

    void Start()
    {
        if (directionUp)
            arrowDown.SetActive(false);
        else
            arrowUp.SetActive(false);
        Disable();
    }

    public void SetText(string val)
    {
        this.val = val;
        innerText.text = val;
    }

    public void SetPosition(float x, float y, bool directionUp=true)
    {
        SetDirection(directionUp);
        descriptor.gameObject.transform.position = new Vector3(x, y, 0);
        descriptor.SetActive(true);
    }

    private void SetDirection(bool directionUp)
    {
        if(directionUp != this.directionUp)
        {
            this.directionUp = directionUp;
            if (directionUp)
            {
                arrowUp.SetActive(true);
                arrowDown.SetActive(false);
            }
            else
            {
                arrowUp.SetActive(false);
                arrowDown.SetActive(true);
            }
        }
    }

    public void Disable()
    {
        descriptor.SetActive(false);
    }
}
