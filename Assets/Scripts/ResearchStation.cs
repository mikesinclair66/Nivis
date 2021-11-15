using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchStation : MonoBehaviour
{
    GameObject leftBtn, rightBtn, btn2, btn3;
    GameObject pageName;
    //GameObject b1u1, b1u2, b1u3, b2u1, b2u2, b2u3;
    GameObject[] b1Img, b2Img;
    GameObject[] b1, b2;
    int page = 0;
    const int LAST_PAGE = 2;
    Text price1Text, price2Text, price3Text;
    public int price1, price2, price3;

    public static bool[,,] researched;

    void Start()
    {
        leftBtn = GameObject.Find("Canvas/ResearchStation/InnerEl/Row1/Btn1");
        rightBtn = GameObject.Find("Canvas/ResearchStation/InnerEl/Row1/Btn4");
        pageName = GameObject.Find("Canvas/ResearchStation/InnerEl/Row1/PageName");
        b1Img = new GameObject[3];
        b2Img = new GameObject[3];
        b1 = new GameObject[3];
        b2 = new GameObject[3];
        b1Img[0] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row2/1a/Image");
        b1Img[1] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row3/1a/Image");
        b1Img[2] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row4/1a/Image");
        b2Img[0] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row2/1b/Image");
        b2Img[1] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row3/1b/Image");
        b2Img[2] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row4/1b/Image");
        b1[0] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row2/1a");
        b1[1] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row3/1a");
        b1[2] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row4/1a");
        b2[0] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row2/1b");
        b2[1] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row3/1b");
        b2[2] = GameObject.Find("Canvas/ResearchStation/InnerEl/Row4/1b");
        price1Text = GameObject.Find("Canvas/ResearchStation/InnerEl/Row2/Cost").GetComponent<Text>();
        price2Text = GameObject.Find("Canvas/ResearchStation/InnerEl/Row3/Cost").GetComponent<Text>();
        price3Text = GameObject.Find("Canvas/ResearchStation/InnerEl/Row4/Cost").GetComponent<Text>();

        price1Text.text = price1.ToString("0");
        price2Text.text = price2.ToString("0");
        price3Text.text = price3.ToString("0");

        researched = new bool[LAST_PAGE + 1, 2, 3];//page,branch,upgrades
        for (int i = 0; i < LAST_PAGE + 1; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                researched[i, 0, j] = false;
                researched[i, 1, j] = false;
            }
        }

        for(int i = 0; i < 3; i++)
        {
            //change to button objects
            b1[i].gameObject.transform.position = b1[i].gameObject.transform.position
                + new Vector3(-1000, 0, 0);
            b2[i].gameObject.transform.position = b2[i].gameObject.transform.position
                + new Vector3(-200, 0, 0);
        }
        UpdatePage(page);
    }

    void UpdatePage(int page)
    {
        Text pageName = this.pageName.GetComponent<Text>();

        switch (page)
        {
            case 0:
                pageName.text = "Standard\nUpgrades";
                break;
            case 1:
                pageName.text = "Missile\nUpgrades";
                break;
            case 2:
            default:
                pageName.text = "Radiator\nUpgrades";
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            if (researched[page, 0, i])
                b1Img[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            else
                b1Img[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.25f);

            if (researched[page, 1, i])
                b2Img[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            else
                b2Img[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
        }
    }

    public void NextPage()
    {
        if (++page > LAST_PAGE)
            page = 0;
        UpdatePage(page);
    }

    public void PrevPage()
    {
        if (--page < 0)
            page = LAST_PAGE;
        UpdatePage(page);
    }

    public void UpgradeClicked(int upgradeNo)
    {
        int i;
        bool primaryBranch = upgradeNo < 3;
        switch (upgradeNo)
        {
            case 0:
            case 3:
                i = 0;
                break;
            case 1:
            case 4:
                i = 1;
                break;
            case 2:
            case 5:
            default:
                i = 2;
                break;
        }

        if (primaryBranch)
        {
            if (i > 0)
            {
                for (int n = i - 1; n > -1; n--)
                    if (!researched[page, 0, n])
                    {
                        Debug.Log("You must upgrade from the start of the tree before upgrading further.");
                        return;
                    }

                researched[page, 0, i] = true;
            }
            else
                researched[page, 0, 0] = true;
        }
        else
        {
            if (i > 0)
            {
                for (int n = i - 1; n > -1; n--)
                    if (!researched[page, 1, n])
                    {
                        Debug.Log("You must upgrade from the start of the tree before upgrading further.");
                        return;
                    }

                researched[page, 1, i] = true;
            }
            else
                researched[page, 1, 0] = true;
        }

        UpdatePage(page);
    }
}