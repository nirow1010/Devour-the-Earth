using UnityEngine;

public class MinionElectricOrb : Projectile
{
    public float stunTime;
    private GameObject tempHitEffect;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        State state = collider.GetComponent<State>();

        if (state == null)
        {
            Destroy(gameObject, 0.1f);
        }
        else
        {
            state.TakeDamage(GetDamage());

            if (state is Stunnable && hitEffect != null)
            {
                ((Stunnable)state).GetStunned(stunTime);

                if (tempHitEffect == null)
                    Destroy(tempHitEffect);

                tempHitEffect = Instantiate(hitEffect, collider.transform.position, collider.transform.rotation);
                SelfDestructiveFollowingEffect sd = tempHitEffect.GetComponent<SelfDestructiveFollowingEffect>();

                if (sd != null)
                    sd.SetFollow(collider.transform);

                Destroy(tempHitEffect, stunTime);
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
