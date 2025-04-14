using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        State state = collider.gameObject.GetComponent<State>();

        if (state != null)
        {
            state.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
