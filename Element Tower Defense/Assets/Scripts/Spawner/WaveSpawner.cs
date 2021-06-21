using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Enemy and Spawn informations
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform enemySpawnPoint;

    // Wave information
    private int currentWaveNumber = 1;
    private int difficultyMultiplyer = 2;
    private float waitTime = 0.75f;
    private List<GameObject> listOfEnemies = new List<GameObject>();

    // Wave timer
    private int nextWaveCountdown = 60;
    private float countdown;

    // UI
    private GameUI gameUI;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = gameObject.GetComponent<GameUI>();
        countdown = nextWaveCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<PlayerStats>().IsGameOver()) 
        { 
            if (GetListOfEnemies().Count <= 0)
            {
                gameUI.ChangeWaveSpawnBtnStage(true);
                gameUI.ChangeCountdownTextStat(true);
                if (countdown <= 0)
                {
                    countdown = nextWaveCountdown;
                    StartCoroutine(SpawnWave());
                }
                gameUI.TimerTextUpdate(countdown);
                countdown -= Time.deltaTime;
            }
            else
            {
                gameUI.ChangeCountdownTextStat(false);
                gameUI.ChangeWaveSpawnBtnStage(false);
            }
        } 
        else
        {
            gameUI.ChangeCountdownTextStat(false);
            gameUI.ChangeWaveSpawnBtnStage(false);
        }
    }

    // Public Functions
    public void SpawnWaveOverUI()
    {
        countdown = nextWaveCountdown;
        gameUI.TimerTextUpdate(countdown);
        gameUI.ChangeWaveSpawnBtnStage(false);
        StartCoroutine(SpawnWave());
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

    // Private Functions
    private GameObject SpawnEnemy()
    {
        return Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation).gameObject;
    }

    // Interfaces
    IEnumerator SpawnWave()
    {
        currentWaveNumber++;
        gameUI.UpdateWaveNumberInUI();
        for (int i = 0; i < currentWaveNumber + (currentWaveNumber * difficultyMultiplyer); i++)
        {
            listOfEnemies.Add(SpawnEnemy());
            yield return new WaitForSeconds(waitTime);
        }
    }
}
