using UnityEngine;

public class AssimilationBullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyState enemy = collider.gameObject.GetComponent<EnemyState>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
