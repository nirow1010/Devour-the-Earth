using UnityEngine;

public class MinionElectricOrb : Projectile
{
    public float stunTime;
    private GameObject tempHitEffect;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (hitEffect != null)
        {
            if (tempHitEffect == null)
                Destroy(tempHitEffect);

            tempHitEffect = Instantiate(hitEffect, collider.transform.position, collider.transform.rotation);
            Destroy(tempHitEffect, stunTime);
        }

        State state = collider.GetComponent<State>();

        if (state != null)
        {
            if (state is EarthState)
            {
                Destroy(gameObject, 2);
            }

            state.TakeDamage(GetDamage());

            if (state is Stunnable)
            {
                ((Stunnable) state).GetStunned(stunTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
