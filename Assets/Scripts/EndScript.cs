using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    public Text waveText;

    void Start()
    {
        waveText.text = "You survived " + WaveSpawner.lastWave.ToString() + " waves.";
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
