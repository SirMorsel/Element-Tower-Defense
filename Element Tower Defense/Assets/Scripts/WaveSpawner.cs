using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemySpawnPoint;
    private int waveID = 1;
    private int difficultyMultiplyer = 2;
    private float waitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWaveOverUI()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveID + (waveID * difficultyMultiplyer); i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitTime);
        }
        waveID++;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }
}
