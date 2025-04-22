using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPathfinding : MonoBehaviour
{
    enum EnemyState { Idle, Chase, Close }
    EnemyState currentState = EnemyState.Idle;

    Transform player;
    Transform centerObject;

    private float farRange;
    public float closeRange;
    private float chaseRange;
    public float attackRange;
    public float viewRange;

    public float separationRadius;
    public float separationStrength;

    private float speed;
    public float chaseSpeed;
    public float lingerSpeed;
    public float idleSpeed;
    public int earthOrbitRadius;

    private Vector2 oldVelocity = Vector2.zero;
    private Vector2 orbitCenter = new Vector2(20, 10);

    private Rigidbody2D rb;

    private Collider2D[] nearMe;
    private Collider2D[] inView;

    public LayerMask avoidLayer;
    public LayerMask shootAtLayer;

    public bool chaseWhenHit;
    public bool faceWhenHit;
    private bool chaseingDoToHit;
    private bool faceingDoToHit;

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        GameObject earthObj = GameObject.FindWithTag("Earth");
        if (earthObj != null) centerObject = earthObj.transform;

        rb = GetComponent<Rigidbody2D>();
        chaseRange = attackRange + 2;
        farRange = chaseRange;
    }

    void FixedUpdate()
    {
        bool inClose = Physics2D.OverlapCircle(transform.position, closeRange, shootAtLayer);
        bool inEnterChase = Physics2D.OverlapCircle(transform.position, chaseRange, shootAtLayer);
        bool inExitChase = Physics2D.OverlapCircle(transform.position, attackRange, shootAtLayer);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (inEnterChase || chaseingDoToHit)
                    currentState = EnemyState.Chase;
                break;

            case EnemyState.Chase:
                if (inClose)
                    currentState = EnemyState.Close;
                else if (!inExitChase && !chaseingDoToHit)
                    currentState = EnemyState.Idle;
                break;

            case EnemyState.Close:
                if (!inClose)
                    currentState = EnemyState.Chase;
                break;
        }

        if (player == null)
        {
            currentState = EnemyState.Idle;
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
                farRange = chaseRange;
                moveDir = (player.position - transform.position).normalized;
                speed = chaseSpeed;
                break;
            case EnemyState.Close:
                Vector2 orbit = GetOrbitDirectionAroundPlayer(player.position);
                Vector2 idleDrift = Random.insideUnitCircle * 0.2f;
                moveDir = (orbit + idleDrift).normalized;
                speed = lingerSpeed;
                chaseingDoToHit = false;
                faceingDoToHit = false;
                break;
        }

        Vector2 separation = GetSeparationForce();
        Vector2 finalMove = (moveDir + separation).normalized;

        Vector2 smoothedVelocity = Vector2.SmoothDamp(rb.linearVelocity, finalMove * speed, ref oldVelocity, 0.1f);
        rb.linearVelocity = smoothedVelocity;

        GameObject target = FindClosestTarget();
        Quaternion targetRotation;

        if (player != null && target != null)
        {
            Vector2 angleDirection = player.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;
            targetRotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else
        {
            Vector2 angleDirection = orbitCenter - (Vector2) transform.position;
            float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg + 90f;
            targetRotation = Quaternion.Euler(0, 0, targetAngle);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
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
        nearMe = Physics2D.OverlapCircleAll(transform.position, separationRadius, avoidLayer);
        Vector2 force = Vector2.zero;

        foreach (Collider2D obj in nearMe)
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

    GameObject FindClosestTarget()
    {
        inView = Physics2D.OverlapCircleAll(transform.position, viewRange, shootAtLayer);
        GameObject closest = null;
        float shortestDistance = viewRange;

        foreach (Collider2D obj in inView)
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
            if (obj.CompareTag("Player") && !chaseWhenHit)
            {
                faceingDoToHit = false;
            }
        }
        if (faceingDoToHit)
        {
                closest = player.gameObject;
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

    public void setAttackingDoToHit()
    {
        if(chaseWhenHit)
            chaseingDoToHit = true;
        if (faceWhenHit)
            faceingDoToHit = true;
    }
    public bool getFaceingDoToHit()
    {
        return faceingDoToHit;
    }
    public bool IsOnFight()
    {
        return inView != null && inView.Length > 0;
    }
}
