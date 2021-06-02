using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform EnemyPrefab;
    public Transform EnemySpawnPoint;

    private int currentWaveNumber = 1;
    private int difficultyMultiplyer = 2;
    private float waitTime = 0.75f;

    // Wave timer
    private int nextWaveCountdown = 60;
    private float countdown;

    private List<GameObject> listOfEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        countdown = nextWaveCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<PlayerStats>().IsGameOver()) 
        { 
            if (GetListOfEnemies().Count <= 0)
            {
                gameObject.GetComponent<GameUI>().ChangeWaveSpawnBtnStage(true);
                gameObject.GetComponent<GameUI>().ChangeCountdownTextStat(true);
                if (countdown <= 0)
                {
                    countdown = nextWaveCountdown;
                    StartCoroutine(SpawnWave());
                }
                gameObject.GetComponent<GameUI>().TimerTextUpdate(countdown);
                countdown -= Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<GameUI>().ChangeCountdownTextStat(false);
                gameObject.GetComponent<GameUI>().ChangeWaveSpawnBtnStage(false);
            }
        } 
        else
        {
            gameObject.GetComponent<GameUI>().ChangeCountdownTextStat(false);
            gameObject.GetComponent<GameUI>().ChangeWaveSpawnBtnStage(false);
        }
    }

    public void SpawnWaveOverUI()
    {
        countdown = nextWaveCountdown;
        gameObject.GetComponent<GameUI>().TimerTextUpdate(countdown);
        gameObject.GetComponent<GameUI>().ChangeWaveSpawnBtnStage(false);
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
