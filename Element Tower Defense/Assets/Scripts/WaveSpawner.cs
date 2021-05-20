using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // [System.Serializable]
    public Transform EnemyPrefab;
    public Transform EnemySpawnPoint;

    private int currentWaveNumber = 1;
    private int difficultyMultiplyer = 2;
    private float waitTime = 0.75f;

    private List<GameObject> listOfEnemies = new List<GameObject>();
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
        currentWaveNumber++;
        for (int i = 0; i < currentWaveNumber + (currentWaveNumber * difficultyMultiplyer); i++)
        {
            listOfEnemies.Add(SpawnEnemy());
            yield return new WaitForSeconds(waitTime);
        }
        // print($"TEST LIST{listOfEnemies[0]}");
    }

    private GameObject SpawnEnemy()
    {
        return Instantiate(EnemyPrefab, EnemySpawnPoint.position, EnemySpawnPoint.rotation).gameObject;
    }

    public int GetWaveNumber()
    {
        return currentWaveNumber;
    }

    public List<GameObject> GetListOfEnemies()
    {
        return listOfEnemies;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        listOfEnemies.Remove(enemy);
    }
}
