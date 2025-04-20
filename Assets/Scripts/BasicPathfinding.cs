using UnityEngine;

public class BasicPathfinding : MonoBehaviour
{
    public GameObject goal;
    public float speed;

    public bool isenemy;

    public string target;
    public float roateSpeed;
    public float spaceBetween;

    void Start()
    {
        if (isenemy)
            goal = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(goal.transform.position, this.transform.position) >= spaceBetween)
        {
            Vector2 direction = goal.transform.position - this.transform.position;
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
        }

        GameObject target = FindClosestTarget();
        if (target != null)
        {
            Vector2 angleDirection = target.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(target);
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

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
}
