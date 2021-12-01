using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUIHandler : MonoBehaviour
{
    public GameObject gameMenu;

    void Start()
    {
        gameMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameMenu.activeSelf == true)
            {
                gameMenu.SetActive(false);
            }
            else
            {
                gameMenu.SetActive(true);
            }
        }
    }

    public void restartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
