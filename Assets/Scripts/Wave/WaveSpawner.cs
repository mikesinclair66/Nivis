using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public GameObject enemy;
    public GameObject timeController;
    public GameObject timePauseButton;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text waveCountdownText;
    public Text waveIndexText;

    public static int waveIndex = 0;

    void Start()
    {
        countdown = 5f;
        waveIndex = 0;
        EnemiesAlive = 0;
    }

    void Update()
    {
        Debug.Log("Enemies alive: " + EnemiesAlive);
        if (EnemiesAlive > 0) { return; }

        if (TimeControl.autoStart == false)
        {
            timeController.GetComponent<TimeControl>().Pause();
        }
        else
        {
            if (countdown <= 0f)
            {
                timePauseButton.GetComponent<Button>().interactable = false;
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
            else
                timePauseButton.GetComponent<Button>().interactable = true;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        if (waveIndex < waves.Length)
        {
            Wave wave = waves[waveIndex];
            int waveIndexPlusOne = waveIndex + 1;
            waveIndexText.text = "Wave: " + waveIndexPlusOne.ToString() + "/" + waves.Length.ToString();
            foreach (WaveEnemy i in wave.waveEnemies)
            {
                for (int j = 0; j < i.count; j++)
                {
                    SpawnEnemy(i.enemy);
                    yield return new WaitForSeconds(1f / i.rate);
                }
            }
            waveIndex++;
        }
        else
        {
            SceneManager.LoadScene("Victory");
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
