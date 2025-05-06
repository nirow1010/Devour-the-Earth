using UnityEngine;

public class BasicPathfinding : MonoBehaviour
{
    private GameObject goal;
    private GameObject earth;
    private GameObject player;

    public float speed;
    public bool faceForward;

    public string target;
    public float rotateSpeed;
    private float spaceBetween;
    private MinionState minionState;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        earth = GameObject.FindWithTag("Earth");
        minionState = this.GetComponent<MinionState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goal != null && Vector2.Distance(goal.transform.position, this.transform.position) >= spaceBetween)
        {
            Vector2 direction = goal.transform.position - this.transform.position;
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
        }


        if (faceForward)
        {
            if (player != null)
            {
                Quaternion finalRotation;
                if (IsZeroQuaternion(minionState.shipData.minionAngle))
                { 
                    finalRotation = player.transform.rotation;
                }
                else
                {
                    finalRotation = player.transform.rotation * minionState.shipData.minionAngle;
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, rotateSpeed * Time.deltaTime);
            }
        }
        else 
        {
            GameObject target = FindClosestTarget();
            if (target != null)
            {
                Vector2 angleDirection = target.transform.position - transform.position;
                float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotateSpeed * Time.deltaTime);
            }
        }
    }

    GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(target);

        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        if (earth != null)
        {
            closest = earth;
            shortestDistance = (transform.position - closest.transform.position).magnitude;
        }

        foreach (GameObject t in targets)
        {
            float dist = Vector2.Distance(transform.position, t.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closest = t;
            }
        }

        return closest;
    }
    bool IsZeroQuaternion(Quaternion q)
    {
        return q.x == 0f && q.y == 0f && q.z == 0f && q.w == 0f;
    }
    public void SetGoal(GameObject goal)
    {
        this.goal = goal;
    }
}
