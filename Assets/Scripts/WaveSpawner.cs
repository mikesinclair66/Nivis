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

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text waveCountdownText;
    public Text waveIndexText;

    public static int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0) { return; }

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
        Debug.Log("WAVE INDEX: " + waveIndex);
        Debug.Log("WAVE LENGTH: " + waves.Length);
        if (waveIndex < waves.Length)
        {
            Wave wave = waves[waveIndex];

            //enemy.GetComponent<Enemy>().ScaleHP();

            waveIndexText.text = "Wave: " + waveIndex.ToString();
            waveIndexText.text = "R" + waveIndex.ToString();

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
