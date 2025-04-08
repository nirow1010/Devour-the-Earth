using System.Collections;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int startingHealth = 5;
    [SerializeField] Color hitColor;

    private int health;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startingHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(ReactOnHit());
    }

    private IEnumerator ReactOnHit()
    {
        sr.color = hitColor;

        yield return new WaitForSeconds(0.2f);

        if (health < 0)
        {
            GetComponent<DeathScript>().assimilate();
            Destroy(gameObject);
        }

        sr.color = Color.white;
    }
}
