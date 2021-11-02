using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    public Text waveText;
    private int waveSurvived = WaveSpawner.waveIndex - 1;

    void Start()
    {
        waveText.text = "You survived " + waveSurvived.ToString() + " waves.";
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
