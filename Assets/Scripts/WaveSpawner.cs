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
        if (waveIndex == waves.Length)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            Wave wave = waves[waveIndex];

            enemy.GetComponent<Enemy>().ScaleHP();

            waveIndexText.text = "Wave: " + waveIndex.ToString();
            waveIndexText.text = "R" + waveIndex.ToString();

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }

            waveIndex++;

            //Debug.Log("waveIndex: " + waveIndex);
            //Debug.Log("waves.Length: " + waves.Length);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
