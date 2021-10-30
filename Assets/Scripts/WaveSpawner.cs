using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public GameObject enemy;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text waveCountdownText;
    public Text waveIndexText;

    public int waveIndex = 0;
    public int winningWave = 34;
    public static int lastWave;

    void Update()
    {
        if (EnemiesAlive > 0){return;}

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        if (waveIndex > winningWave)
        {
            SceneManager.LoadScene("Victory");
        }

        enemy.GetComponent<Enemy>().ScaleHP(waveIndex);
        waveIndex++;

        lastWave = waveIndex; //used in EndScript
        waveIndexText.text = "Wave: " + waveIndex.ToString();
        lastWave = waveIndex;
        waveIndexText.text = "R" + waveIndex.ToString();
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
