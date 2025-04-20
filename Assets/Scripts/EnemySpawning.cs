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

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(10f);
        Instantiate(enemy, new Vector3(20,10,0), new Quaternion(0,0,0,0));
        StartCoroutine(Spawn());
    }

}
