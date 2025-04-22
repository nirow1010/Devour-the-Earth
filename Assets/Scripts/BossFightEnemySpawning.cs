using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossFightEnemySpawning : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public GameObject prefab;

        public int orbitHighBound;
        public int orbitLowBound;

        public int spawnAmountHighBound;
        public int spawnAmountLowBound;
    }

    [System.Serializable]
    public class EnemyLevels
    {
        public List<Enemy> easyEnemys = new List<Enemy>();
        public List<Enemy> mediumEnemys = new List<Enemy>();
        public List<Enemy> hardEnemys = new List<Enemy>();
    }

    [System.Serializable]
    public struct TimeRange
    {
        public int min;
        public int max;
    }
    [System.Serializable]
    public struct DifficultyWeight
    {
        public int easy;
        public int medium;
        public int hard;
    }

    public int totalPhase
    {
        get
        {
            return totalPhase;
        }
        set
        {
            List<EnemyLevels> temp1 = new List<EnemyLevels>(value);
            TimeRange[] temp2 = new TimeRange[value];
            DifficultyWeight[] temp3 = new DifficultyWeight[value];

            for (int i = 0; i < value || i < totalPhase; i++)
            {

            }
        }
    }

    public List<EnemyLevels> phaseEnemyOptions;
    public TimeRange[] timeBetween;
    public DifficultyWeight[] difficultyWeight;

    public EarthState earthState;

    private int phase;
    private System.Random rand;
    public bool readlyToSpawn = true;

    void Start()
    {
        earthState = GetComponent<EarthState>();
        rand = new System.Random();
        readlyToSpawn = true;
        phase = 0;
    }

    void Update()
    {
        float relativeEarthHealth = (earthState.GetHealth() / earthState.startingHealth) * 100;

        while (relativeEarthHealth <= 100f - 100f / totalPhase * (phase + 1))
        {
            phase++;
        }

        if (readlyToSpawn)
            StartCoroutine(SpawnEnemy());
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator SpawnEnemy()
    {
        readlyToSpawn = false;
        int wightTotal = difficultyWeight[phase].easy + difficultyWeight[phase].medium + difficultyWeight[phase].hard;
        int randDificulty = rand.Next(0, wightTotal);
        List<Enemy> enemies;

        if (randDificulty < difficultyWeight[phase].easy)
        {
            enemies = phaseEnemyOptions[phase].easyEnemys;
        }
        else if (randDificulty < difficultyWeight[phase].easy + difficultyWeight[phase].medium)
        {
            enemies = phaseEnemyOptions[phase].mediumEnemys;
        }
        else
        {
            enemies = phaseEnemyOptions[phase].hardEnemys;
        }

        Enemy randEnemy = enemies[rand.Next(0, enemies.Count)];
        int randCount = rand.Next(randEnemy.spawnAmountLowBound,randEnemy.spawnAmountHighBound+1);
        int randOrbit = rand.Next(randEnemy.orbitLowBound, randEnemy.orbitHighBound+1);

        for (int i = 0; i < randCount; i++)
        {
            GameObject newEnemy = Instantiate(randEnemy.prefab);
            newEnemy.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
            yield return null; // Wait one frame so Awake/Start can run

            EnemyPathfinding enemyPathfinding = newEnemy.GetComponentInChildren<EnemyPathfinding>();
            enemyPathfinding.earthOrbitRadius = randOrbit;
            yield return new WaitForSeconds(waitTimeForEqualOrbit(randOrbit, randCount, enemyPathfinding.idleSpeed));
        }

        float randWait = rand.Next(timeBetween[phase].min, timeBetween[phase].max);
        StartCoroutine(Wait(randWait));
        readlyToSpawn = true;
    }

    public float waitTimeForEqualOrbit(int radius, int count, float speed)
    {
        float circumference = 2 * Mathf.PI * radius;
        float distanceBetween = circumference / count;
        float timeBetweenSpawns = distanceBetween / speed;
        return timeBetweenSpawns;
    }

}
