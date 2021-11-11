using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchStation : MonoBehaviour
{
    GameObject leftBtn, rightBtn, btn2, btn3;

    void Start()
    {
        leftBtn = GameObject.Find("Canvas/ResearchStation/InnerEl/Row1/Btn1");
        rightBtn = GameObject.Find("Canvas/ResearchStation/InnerEl/Row1/Btn4");
    }
}
