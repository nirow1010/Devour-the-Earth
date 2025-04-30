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

    private GameObject kamikazeTarget;
    public float homingRotationSpeed = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(GetDamage(), 0f, 0f);
        rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (onKamikaze)
        {
            Vector2 moveDir = transform.up;
            Vector2 moveForce = (moveDir * maxLaunchSpeed - rb.linearVelocity) * accelRate;
            rb.AddForce(moveForce, ForceMode2D.Force);

            if (kamikazeTarget != null)
            {
                Vector2 targetDirection = kamikazeTarget.transform.position - transform.position;
                Vector3 targetRotation = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-targetDirection.x, targetDirection.y));
                Quaternion targetRot = Quaternion.Euler(targetRotation);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, homingRotationSpeed * Time.fixedDeltaTime);
            }

            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.9f, explosionLayerMask);

            if (hit != null || Time.time - counter >= autoDestructionTime)
            {
                Explode();
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

        if (GetComponent<MinionState>() != null && GetComponent<MinionState>().shipData != null)
            Assimilation.minionList.Remove(GetComponent<MinionState>().shipData);

        GlobalStats.enemiesOnSkreen--;

        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }

    protected void SetKamikazeTarget(GameObject target)
    {
        kamikazeTarget = target;
    }
}
