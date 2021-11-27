using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    Vector3 pos, size, rotation;
    bool animating = false, scaling = false;
    Vector3 animScale = new Vector3(0, 0, 0);
    Vector3 animRot = new Vector3(0, 0, 0);
    GameObject upgradeBtn, sellBtn;
    GameObject upgradeImg, sellImg;
    int mode = 0;//0-2

    void Start()
    {
        pos = transform.position;
        size = transform.localScale;
        rotation = transform.localRotation.eulerAngles;
        upgradeBtn = GameObject.Find("TowerUI/Screen/UI/UpgradeBtn");
        sellBtn = GameObject.Find("TowerUI/Screen/UI/SellBtn");
        upgradeImg = GameObject.Find("TowerUI/Screen/UI/UpgradeBtn/UpgradeImg");
        sellImg = GameObject.Find("TowerUI/Screen/UI/SellBtn/SellImg");
        Introduce();
    }

    void FrameCube()
    {
        float thickness = size.z;
        GameObject[] frames = new GameObject[4];
        for (int i = 0; i < 4; i++)
            frames[i] = GameObject.Find("TowerUI/Frame").transform.GetChild(i).gameObject;

        frames[0].transform.position = new Vector3(
            pos.x,
            pos.y + size.y * 0.5f - thickness * 0.5f,
            pos.z - thickness * 0.5f
        );
        frames[1].transform.position = new Vector3(
            pos.x + size.x * 0.5f - thickness * 0.5f,
            pos.y,
            pos.z - thickness * 0.5f
        );
        frames[2].transform.position = new Vector3(
            pos.x,
            pos.y - size.y * 0.5f + thickness * 0.5f,
            pos.z - thickness * 0.5f
        );
        frames[3].transform.position = new Vector3(
            pos.x - size.x * 0.5f + thickness * 0.5f,
            pos.y,
            pos.z - thickness * 0.5f
        );

        frames[0].transform.localScale = new Vector3(
            ((!scaling) ? size.x : animScale.x),
            thickness,
            thickness
        );
        frames[1].transform.localScale = new Vector3(
            thickness,
            ((!scaling) ? size.y : animScale.y),
            thickness
        );
        frames[2].transform.localScale = new Vector3(
            ((!scaling) ? size.x : animScale.x),
            thickness,
            thickness
        );
        frames[3].transform.localScale = new Vector3(
            thickness,
            ((!scaling) ? size.y : animScale.y),
            thickness
        );

        for (int i = 0; i < 4; i++)
        {
            frames[i].transform.eulerAngles = new Vector3(0, 0, 0);
            if (rotation.x != 0)
                frames[i].transform.RotateAround(pos, Vector3.right,
                    ((!scaling) ? rotation.x : animRot.x));
            if (rotation.y != 0)
                frames[i].transform.RotateAround(pos, Vector3.up,
                    ((!scaling) ? rotation.y : animRot.y));
            if (rotation.z != 0)
                frames[i].transform.RotateAround(pos, Vector3.forward,
                    ((!scaling) ? rotation.z : animRot.z));
        }
    }

    void Introduce()
    {
        animating = true;
        scaling = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        upgradeImg.GetComponent<Image>().color =
            new Color(1.0f, 1.0f, 1.0f, 0.0f);
        sellImg.GetComponent<Image>().color =
            new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if (animating)
        {
            if (scaling)
            {
                float inc = 1.75f * Time.deltaTime;
                animScale.x += size.x * inc;
                animScale.y += size.y * inc;
                animScale.z += size.z * inc;
                animRot.x += rotation.x * inc;
                animRot.y += rotation.y * inc;
                animRot.z += rotation.z * inc;
                transform.localScale = animScale;
                transform.eulerAngles = rotation;
                FrameCube();
            }
            else
            {
                upgradeImg.GetComponent<Image>().color =
                        new Color(1.0f, 1.0f, 1.0f, 1.0f);
                sellImg.GetComponent<Image>().color =
                        new Color(1.0f, 1.0f, 1.0f, 1.0f);
                animating = false;
            }

            if (transform.localScale.x >= size.x || transform.localScale.y
                >= size.y || transform.localScale.z >= size.z)
            {
                scaling = false;
                transform.eulerAngles = rotation;
                transform.localScale = size;
                FrameCube();
            }
        }
    }

    public void Upgrade()
    {
        SetMode(1);
        Debug.Log("Upgrade button pressed");
    }

    public void Sell()
    {
        Debug.Log("Sell button pressed");
    }

    /// <summary>
    /// case 0: display both options
    /// case 1: display only sell
    /// case 2: display only upgrade
    /// </summary>
    /// <param name="mode"></param>
    void SetMode(int mode)
    {
        this.mode = mode;
        switch (mode)
        {
            case 0:

                break;
            case 1:
                upgradeBtn.gameObject.SetActive(false);
                sellBtn.transform.Translate(-0.5f, 0, 0);
                break;
            case 2:

                break;
        }
    }
}
