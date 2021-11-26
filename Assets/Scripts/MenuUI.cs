using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    GameObject mainCamera;
    float posX = 0, posZ = 0, rotY = 0;
    float posXDest = 0, posZDest = 0, rotYDest = 0;
    float posXStart = 0, posZStart = -10, rotYStart = 0;
    int cameraAngle = 0;
    bool cameraTransitioning = false, ebHovered = false;
    const float SPEED = 0.25f;
    float elapsedTime = 0, ebElapsedTime = 0;

    GameObject sbFilled, ebFilled, lvl2Filled;
    int textFade = -1;

    GameObject panel;
    GameObject c1, c2, c3, c4;
    GameObject sbText, ebText, lvl2Text;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        sbFilled = GameObject.Find("GUI/Panel/StartButton/ButtonFilled");
        ebFilled = GameObject.Find("GUI/Panel/EndButton/ButtonFilled");
        lvl2Filled = GameObject.Find("GUI/Panel/Lvl2/ButtonFilled");
        panel = GameObject.Find("GUI/Panel");
        c1 = GameObject.Find("GUI/Frame/C1");
        c2 = GameObject.Find("GUI/Frame/C2");
        c3 = GameObject.Find("GUI/Frame/C3");
        c4 = GameObject.Find("GUI/Frame/C4");
        sbText = GameObject.Find("GUI/Panel/StartButton/Text");
        ebText = GameObject.Find("GUI/Panel/ExitButton/Text");

        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        ebFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if ((ebHovered && ebElapsedTime < 1.0f) ||
            (!ebHovered && ebElapsedTime > 0.0f))
        {
            ebElapsedTime += Time.deltaTime * ((ebHovered) ? 1 : -1);
            float ebRatio = ebElapsedTime / SPEED;
            ebFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, ebRatio);
        }

        if (cameraTransitioning)
        {
            elapsedTime += Time.deltaTime;
            float tRatio = ((elapsedTime > SPEED) ? 1 : elapsedTime / SPEED);
            if (tRatio == 1)
            {
                switch (textFade)
                {
                    case 0:
                        //sb fade in
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        break;
                    case 1:
                        //sb fade out
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        break;
                    case 2:
                        //eb fade in
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        break;
                    case 3:
                        //eb fade out
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        break;
                    case 4:
                        //sb fade in, eb fade out
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        break;
                    case 5:
                    default:
                        //eb fade in, sb fade out
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        break;
                }

                posX = posXDest;
                posZ = posZDest;
                rotY = rotYDest;
                cameraTransitioning = false;
            }
            else
            {
                switch (textFade)
                {
                    case 0:
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, tRatio);
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, tRatio);
                        break;
                    case 1:
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (1.0f - tRatio));
                        break;
                    case 2:
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, tRatio);
                        break;
                    case 3:
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (1.0f - tRatio));
                        break;
                    case 4:
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, tRatio);
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (1.0f - tRatio));
                        break;
                    case 5:
                    default:
                        sbFilled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (1.0f - tRatio));
                        lvl2Filled.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, tRatio);
                        break;
                }
                //Debug.Log("tRatio: " + tRatio + ", 1 - tRatio: " + (1.0f - tRatio));

                posX = posXStart + tRatio * ((posXStart <= posXDest) ? 1 : -1) * Math.Abs(posXDest - posXStart);
                posZ = posZStart + tRatio * ((posZStart <= posZDest) ? 1 : -1) * Math.Abs(posZDest - posZStart);
                rotY = rotYStart + tRatio * ((rotYStart <= rotYDest) ? 1 : -1) * Math.Abs(rotYDest - rotYStart);
            }

            mainCamera.transform.position = new Vector3(posX, 0, posZ);
            mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, rotY, 0));
        }
    }

    public void MouseOver(string val)
    {
        if (!cameraTransitioning)
        {
            if (val.Equals("lvl1"))
            {
                if (cameraAngle != 2)
                    SetCameraAngle(2);
            }
            else if (val.Equals("lvl2"))
            {
                if (cameraAngle != 1)
                    SetCameraAngle(1);
            }
        }

        if (val.Equals("exit"))
        {
            ebHovered = true;
            //ensures that the update animation runs
            if (ebElapsedTime == 0.0f)
                ebElapsedTime += 0.002f;
        }
    }

    public void MouseOut(string val)
    {
        if (!cameraTransitioning)
        {
            if (cameraAngle != 0)
                SetCameraAngle(0);
        }

        if (val.Equals("exit"))
        {
            ebHovered = false;
            if (ebElapsedTime == 1.0f)
                ebElapsedTime -= 0.002f;
        }
    }

    public void StartGame(bool lvl1)
    {
        if (lvl1 || !lvl1)
            SceneManager.LoadScene("JackyScene 1", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    void SetCameraAngle(int cameraAngle)
    {
        switch (this.cameraAngle)
        {
            case 0:
                posXStart = 0;
                posZStart = -10;
                rotYStart = 0;
                break;
            case 1:
                posXStart = -252.0f;
                posZStart = 483.0f;
                rotYStart = 21.0f;
                break;
            case 2:
            default:
                posXStart = 252.0f;
                posZStart = 483.0f;
                rotYStart = -21.0f;
                break;
        }

        if (this.cameraAngle == 0 && cameraAngle == 2)
            textFade = 0;
        else if (this.cameraAngle == 2 && cameraAngle == 0)
            textFade = 1;
        else if (this.cameraAngle == 0 && cameraAngle == 1)
            textFade = 2;
        else if (this.cameraAngle == 1 && cameraAngle == 0)
            textFade = 3;
        else if (this.cameraAngle == 1 && cameraAngle == 2)
            textFade = 4;
        else if (this.cameraAngle == 2 && cameraAngle == 1)
            textFade = 5;
        else
            textFade = -1;

        this.cameraAngle = cameraAngle;

        switch (cameraAngle)
        {
            case 0:
                posXDest = 0;
                posZDest = -10.0f;
                rotYDest = 0;
                break;
            case 1:
                posXDest = -252.0f;
                posZDest = 483.0f;
                rotYDest = 21.0f;
                break;
            case 2:
            default:
                posXDest = 252.0f;
                posZDest = 483.0f;
                rotYDest = -21.0f;
                break;
        }

        posX = posXStart;
        posZ = posZStart;
        rotY = rotYStart;
        elapsedTime = 0;
        cameraTransitioning = true;
    }
}
