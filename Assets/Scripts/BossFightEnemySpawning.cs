using NUnit.Framework;
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

    public List<EnemyLevels> phaseEnemyOptions;
    public int phase;
    public EarthState earthState;
    public TimeRange[] timeBetween;
    public DifficultyWeight[] dificultyWight;

    private float relitiveEarthHealth;
    private System.Random rand;
    private bool readlyToSpawn = true;

    void Start()
    {
        earthState = GetComponent<EarthState>();
        rand = new System.Random();
    }

    void Update()
    {
        relitiveEarthHealth = (earthState.GetHealth() / earthState.startingHealth)*100;

        if (relitiveEarthHealth >= 80)
        {
            phase = 0;
        }
        else if (relitiveEarthHealth >= 60)
        {
            phase = 1;
        }
        else if (relitiveEarthHealth >= 40)
        {
            phase = 2;
        }
        else if (relitiveEarthHealth >= 20)
        {
            phase = 3;
        }
        else 
        {
            phase = 4;
        }
        
        if(readlyToSpawn)
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    IEnumerator SpawnEnemy()
    {
        readlyToSpawn = false;
        int wightTotal = dificultyWight[phase].easy + dificultyWight[phase].medium + dificultyWight[phase].hard;
        int randDificulty = rand.Next(0, wightTotal);
        List<Enemy> enemies;
        if (randDificulty < dificultyWight[phase].easy)
        {
            enemies = phaseEnemyOptions[phase].easyEnemys;
        }
        else if (randDificulty < dificultyWight[phase].easy + dificultyWight[phase].medium)
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
            yield return null; // Wait one frame so Awake/Start can run

            newEnemy.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
            EnemyPathfinding enemyPathfinding = newEnemy.GetComponent<EnemyPathfinding>();
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
