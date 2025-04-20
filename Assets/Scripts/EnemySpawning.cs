using System.Collections;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(10f);
        Instantiate(enemy, new Vector3(20,10,0), Quaternion.identity);
        StartCoroutine(Spawn());
    }

}
