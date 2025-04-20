using System.Collections;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public int burstAmount = 2;
    public float burstDelay = 0.5f;
    public float spawnDelay = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        for (int i = 0; i < burstAmount; i++)
        {
            yield return new WaitForSeconds(burstDelay);
            Instantiate(enemy, new Vector3(20, 9, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawn());
    }

}
