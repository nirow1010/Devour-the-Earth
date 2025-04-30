using Unity.Cinemachine;
using UnityEngine;

public class EnemyElectricOrb : Projectile
{
    public float stunTime;
    private GameObject tempHitEffect;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        State state = collider.GetComponent<State>();

        if (state == null)
        {
            Destroy(gameObject);
        }
        else if (state is EarthState)
        {
            Destroy(gameObject, 2);
        }
        else 
        {
            if (hitEffect != null)
            {
                if (tempHitEffect == null)
                    Destroy(tempHitEffect);

                tempHitEffect = Instantiate(hitEffect, collider.transform.position, collider.transform.rotation);
                SelfDestructiveFollowingEffect sd = tempHitEffect.GetComponent<SelfDestructiveFollowingEffect>();

                if (sd != null)
                    sd.SetFollow(collider.transform);

                Destroy(tempHitEffect, stunTime);
            }

            state.TakeDamage(GetDamage());

            if (state is Stunnable)
            {
                ((Stunnable)state).GetStunned(stunTime);
            }
        }
    }
}
