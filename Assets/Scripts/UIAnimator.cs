using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public string innerElName;
    GameObject innerEl;
    Vector3 initScale;
    const float SPEED1 = 0.1f, SPEED2 = 0.2f;
    bool toggled = false, animating = false, secondTransition = false, firstToggle = false;
    float elapsedTime, tRatio;

    // Start is called before the first frame update
    void Start()
    {
        initScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        innerEl = GameObject.Find("Canvas/" + innerElName + "/InnerEl");
        innerEl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > SPEED1 && !secondTransition)
            {
                secondTransition = true;
                elapsedTime = 0;
            }
            else if (!secondTransition)
            {
                if (toggled)
                {
                    tRatio = elapsedTime / SPEED1;
                    gameObject.transform.localScale = new Vector3(tRatio * initScale.x,
                        0.025f, initScale.z);
                }
                else
                {
                    tRatio = (SPEED1 - elapsedTime) / SPEED1;
                    gameObject.transform.localScale = new Vector3(initScale.x,
                        initScale.y * tRatio, initScale.z);
                }
            }

            if (secondTransition)
            {
                if (toggled)
                {
                    tRatio = ((elapsedTime >= SPEED2) ? 1 : elapsedTime / SPEED2);
                    gameObject.transform.localScale = new Vector3(initScale.x,
                        tRatio * initScale.y, initScale.z);
                }
                else
                {
                    tRatio = ((elapsedTime >= SPEED2) ? 0 : (SPEED2 - elapsedTime) / SPEED2);
                    gameObject.transform.localScale = new Vector3(tRatio * initScale.x,
                        0.025f, initScale.z);
                }
            }

            if (elapsedTime >= SPEED1 + SPEED2)
            {
                animating = false;
                innerEl.SetActive(toggled);
            }
        }
    }

    public void Toggle()
    {
        toggled = !toggled;
        secondTransition = false;
        tRatio = 0;
        elapsedTime = 0;
        if (firstToggle)
            innerEl.SetActive(toggled);
        if (firstToggle && toggled)
            gameObject.transform.localScale = new Vector3(initScale.x,
                initScale.y, initScale.z);
        if (!firstToggle)
        {
            animating = true;
            firstToggle = true;
        }
        else if (!toggled)
            animating = true;
    }

    public void RequestToggle()
    {
        if (!toggled)
            Toggle();
    }

    public void CloseUI()
    {
        if (toggled)
            Toggle();
    }
}