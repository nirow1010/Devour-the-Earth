using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class KamikazeSkill : EnemyInstantSkill
{
    [SerializeField] GameObject explosionParticle;

    public float maxLaunchSpeed = 15;
    public float accelRate = 5;
    public float autoDestructionTime = 3;
    public float explosionRadius = 3;

    public LayerMask explosionLayerMask;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool onKamikaze = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(20f, 0f);
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        col.isTrigger = false;
    }

    void FixedUpdate()
    {
        if (onKamikaze)
        {
            Vector2 moveDir = transform.up;
            Vector2 moveForce = (moveDir * maxLaunchSpeed - rb.linearVelocity) * accelRate;
            rb.AddForce(moveForce, ForceMode2D.Force);
        }
    }

    public override void UseSkill()
    {
        col.isTrigger = true;
        StartCoroutine(InitiateKamikaze());
    }

    protected IEnumerator InitiateKamikaze()
    {
        onKamikaze = true;
        yield return new WaitForSeconds(autoDestructionTime);
        Explode();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        StopCoroutine(InitiateKamikaze());
        Explode();
        Destroy(gameObject);
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
