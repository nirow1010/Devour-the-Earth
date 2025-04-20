using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class KamikazeSkill : EnemyInstantSkill
{
    [SerializeField] GameObject explosionParticle;

    public float maxLaunchSpeed = 15;
    public float accelRate = 5;
    public float autoDestructionTime = 3;
    public float explosionRadius = 3;

    public LayerMask explosionLayerMask;

    private Rigidbody2D rb;
    private bool onKamikaze = false;
    private float counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(10f, 0f);
        rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (onKamikaze)
        {
            Vector2 moveDir = transform.up;
            Vector2 moveForce = (moveDir * maxLaunchSpeed - rb.linearVelocity) * accelRate;
            rb.AddForce(moveForce, ForceMode2D.Force);

            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.9f, explosionLayerMask);

            if (hit != null)
            {
                Explode();
                Destroy(gameObject);
            }

            if (Time.time - counter >= autoDestructionTime)
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }

    public override void UseSkill()
    {
        if (!onKamikaze)
        {
            onKamikaze = true;
            counter = Time.time;
        }
    }

    private void Explode()
    {
        GameObject particle = Instantiate(explosionParticle);
        particle.transform.position = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider2D hit in hits)
        {
            State state = hit.GetComponent<State>();

            if (state != null)
            {
                state.TakeDamage(GetDamage());
            }
        }
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }
}
