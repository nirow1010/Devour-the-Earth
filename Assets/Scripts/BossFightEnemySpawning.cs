using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;

public class BossFightEnemySpawning : MonoBehaviour
{
    // initial spawn setting
    public bool readyToSpawn = true;
    
    // Phase Settings
    public int totalPhase;
    [SerializeField, SyncedArrayAttribute("totalPhase")] PhaseSetting[] phaseSettings;

    // Stores Earth Information
    [SerializeField] EarthState earthState;

    // Stores private variables
    private int phase;
    private System.Random rand;

    void Start()
    {
        earthState = GetComponent<EarthState>();
        rand = new System.Random();
        readyToSpawn = true;
        phase = 0;
    }

    void Update()
    {
        float relativeEarthHealth = earthState.GetHealth() / earthState.startingHealth;
        phase = (int) ((1f - relativeEarthHealth) * totalPhase);
        if (readyToSpawn)
            StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        PhaseSetting phaseSetting = phaseSettings[phase];

        PhaseSetting.EnemyOptions options = phaseSetting.GetEnemyOptions();
        PhaseSetting.TimeRange timeRange = phaseSetting.GetTimeRange();
        PhaseSetting.DifficultyWeight weight = phaseSetting.GetDifficultyWeight();

        readyToSpawn = false;

        int weightTotal = weight.easy + weight.medium + weight.hard;
        int randDificulty = rand.Next(0, weightTotal);

        List<Enemy> enemies;

        if (randDificulty < weight.easy)
        {
            enemies = options.easyEnemies;
        }
        else if (randDificulty < weight.easy + weight.medium)
        {
            enemies = options.mediumEnemies;
        }
        else
        {
            enemies = options.hardEnemies;
        }

        Enemy randEnemy = enemies[rand.Next(0, enemies.Count)];

        int randCount = rand.Next(randEnemy.spawnAmountLowBound,randEnemy.spawnAmountHighBound + 1);
        int randOrbit = rand.Next(randEnemy.orbitLowBound, randEnemy.orbitHighBound + 1);

        for (int i = 0; i < randCount; i++)
        {
            GameObject newEnemy = Instantiate(randEnemy.prefab);
            newEnemy.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            yield return null; // Wait one frame so Awake/Start can run

            EnemyPathfinding enemyPathfinding = newEnemy.GetComponentInChildren<EnemyPathfinding>();
            enemyPathfinding.earthOrbitRadius = randOrbit;
            yield return new WaitForSeconds(waitTimeForEqualOrbit(randOrbit, randCount, enemyPathfinding.idleSpeed));
        }

        float randWait = rand.Next(timeRange.min, timeRange.max);
        yield return new WaitForSeconds(randWait);
        readyToSpawn = true;
    }

    public float waitTimeForEqualOrbit(int radius, int count, float speed)
    {
        float circumference = 2 * Mathf.PI * radius;
        float distanceBetween = circumference / count;
        float timeBetweenSpawns = distanceBetween / speed;
        return timeBetweenSpawns;
    }

    [System.Serializable]
    private class Enemy
    {
        public GameObject prefab;

        public int orbitHighBound;
        public int orbitLowBound;

        public int spawnAmountHighBound;
        public int spawnAmountLowBound;
    }

    [System.Serializable]
    private class PhaseSetting
    {
        [SerializeField] EnemyOptions enemyOptions;
        [SerializeField] TimeRange timeRange;
        [SerializeField] DifficultyWeight difficultyWeight;

        public EnemyOptions GetEnemyOptions()
        {
            return enemyOptions;
        }

        public TimeRange GetTimeRange()
        {
            return timeRange;
        }

        public DifficultyWeight GetDifficultyWeight()
        {
            return difficultyWeight;
        }

        [System.Serializable]
        public class EnemyOptions
        {
            public List<Enemy> easyEnemies = new List<Enemy>();
            public List<Enemy> mediumEnemies = new List<Enemy>();
            public List<Enemy> hardEnemies = new List<Enemy>();
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
    }
}
