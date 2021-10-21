using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public GameObject enemy;

    public float timeBetweenWaves = 10f;
    private float countdown = 5f;

    public Text waveCountdownText;
    public Text waveIndexText;

    public int waveIndex = 0;
    public static int lastWave;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        if (waveIndex > 34)
        {
            SceneManager.LoadScene("Victory");
        }

        enemy.GetComponent<Enemy>().ScaleHP(waveIndex);
        waveIndex++;
        lastWave = waveIndex;
        waveIndexText.text = "Wave: " + waveIndex.ToString();
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
