using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    enum EnemyState { Idle, Chase, Close }
    EnemyState currentState;

    Transform player;
    Transform centerObject;

    private float farRange;
    public float closeRange;
    public float attackRange;
    public float viewRange;
    private float chaceRange;

    public float separationRadius;
    public float separationStrength;
    private float speed;
    public float chaseSpeed;
    public float lingerSpeed;
    public float idleSpeed;
    public int earthOrbitRadius;

    private Vector2 oldVelocity = Vector2.zero;

    public Rigidbody2D rb;
    private Vector2 orbitCenter = new Vector2(20,10);

    private Collider2D playerFar;
    private Collider2D playerClose;

    private Collider2D[] neerMe;
    private Collider2D[] inVeiw;
    

    public LayerMask playerLayer;
    public LayerMask avoidLayer;
    public LayerMask shootAtLayer;


    private void Start()
    {
        chaceRange = attackRange + 2;
        player = GameObject.FindWithTag("Player").transform;
        centerObject = GameObject.FindWithTag("Earth").transform;

        float thisRadius = GetColliderRadius(GetComponent<Collider2D>());
        farRange = chaceRange;

    }

    void FixedUpdate()
    {
        bool inClose = Physics2D.OverlapCircle(transform.position, closeRange, playerLayer);
        bool inEnterChase = Physics2D.OverlapCircle(transform.position, chaceRange, playerLayer);
        bool inExitChase = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (inEnterChase)
                    currentState = EnemyState.Chase;
                break;

            case EnemyState.Chase:
                if (inClose)
                    currentState = EnemyState.Close;
                else if (!inExitChase)
                    currentState = EnemyState.Idle;
                break;

            case EnemyState.Close:
                if (!inClose)
                    currentState = EnemyState.Chase;
                break;
        }

        Vector2 moveDir = Vector2.zero;

        switch (currentState)
        {
            case EnemyState.Idle:
                farRange = attackRange;
                moveDir = GetOrbitDirectionAroundEarth(centerObject.position);
                speed = idleSpeed;
                    break;
            case EnemyState.Chase:
                farRange = chaceRange;
                moveDir = (player.position - transform.position).normalized;
                speed = chaseSpeed;
                break;
            case EnemyState.Close:
                speed = lingerSpeed;
                Vector2 orbit = GetOrbitDirectionAroundPlayer(player.position);
                Vector2 idleDrift = Random.insideUnitCircle * 0.2f;
                moveDir = (orbit + idleDrift).normalized;
                break;
        }

        Vector2 separation = GetSeparationForce();
        Vector2 finalMove = (moveDir + separation).normalized;

        Vector2 smoothedVelocity = Vector2.SmoothDamp(rb.linearVelocity, finalMove * speed, ref oldVelocity, 0.1f);
        rb.linearVelocity = smoothedVelocity;

        GameObject target = FindClosestTarget();
        if (target != null)
        {
                Vector2 angleDirection = target.transform.position - transform.position;
                float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;

                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        }
        else
        {
            Vector2 angleDirection = orbitCenter - (Vector2)transform.position;
            float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle+180);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        }
    }

    Vector2 GetOrbitDirectionAroundPlayer(Vector2 center)
    {
        Vector2 toCenter = center - (Vector2)transform.position;
        float currentDistance = toCenter.magnitude;

        float distanceError = currentDistance - closeRange;

        Vector2 tangent = new Vector2(-toCenter.y, toCenter.x).normalized;

        Vector2 radialCorrection = toCenter.normalized * (distanceError);

        Vector2 adjustedDirection = (tangent + radialCorrection).normalized;

        return adjustedDirection;
    }
    Vector2 GetOrbitDirectionAroundEarth(Vector2 center)
    {
        Vector2 toCenter = center - (Vector2)transform.position;
        float currentDistance = toCenter.magnitude;

        float distanceError = currentDistance - earthOrbitRadius;

        Vector2 tangent = new Vector2(-toCenter.y, toCenter.x).normalized;

        Vector2 radialCorrection = toCenter.normalized * (distanceError);

        Vector2 adjustedDirection = (tangent + radialCorrection).normalized;

        return adjustedDirection;
    }

    Vector2 GetSeparationForce()
    {
        neerMe = Physics2D.OverlapCircleAll(transform.position, separationRadius, avoidLayer);
        Vector2 force = Vector2.zero;

        foreach (Collider2D obj in neerMe)
        {
            if (obj != null && obj.gameObject != gameObject)
            {
                Vector2 offset = (Vector2)(transform.position - obj.transform.position);
                float dist = offset.magnitude;
                if (dist > 0.01f)
                    force += offset.normalized / dist;
            }
        }

        return force * separationStrength;
    }


    float GetColliderRadius(Collider2D col)
    {
        if (col is CircleCollider2D circle)
        {
            return circle.radius * Mathf.Max(col.transform.lossyScale.x, col.transform.lossyScale.y);
        }
        else
        {
            return col.bounds.extents.magnitude;
        }
    }

    GameObject FindClosestTarget()
    {
        inVeiw = Physics2D.OverlapCircleAll(transform.position, viewRange, shootAtLayer);
        GameObject closest = null;
        float shortestDistance = viewRange;

        //if (player != null)
        //{
        //    float dist = Vector2.Distance(transform.position, player.position);
        //    if (dist < shortestDistance)
        //    {
        //        shortestDistance = dist;
        //        closest = player.gameObject;
        //    }
        //}
            foreach (Collider2D obj in inVeiw)
            {
                if (obj.CompareTag("minion") || obj.CompareTag("Player"))
                {
                    float dist = Vector2.Distance(transform.position, obj.transform.position);
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        closest = obj.gameObject;
                    }
                }
            }
            return closest;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.DrawWireSphere(transform.position, closeRange);
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }

    public bool IsOnFight()
    {
        return inVeiw.Length > 0;
    }
}
