using UnityEngine;

public abstract class State : MonoBehaviour
{
    public float startingHealth = 5;
    private float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void GainHealth(float gain)
    {
        health += gain;
    }

    public virtual bool IsAlive()
    {
        return health > 0;
    }

    public virtual float GetHealth()
    {
        return health;
    }
}
