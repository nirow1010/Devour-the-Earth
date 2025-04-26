using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    private float damage = 1;
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Instantiate(hitEffect, transform.position + transform.up * 0.5f, transform.rotation);

        State state = collider.gameObject.GetComponent<State>();
        EarthState earth = collider.gameObject.GetComponent<EarthState>();

        if (state != null && earth == null)
        {
            state.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
