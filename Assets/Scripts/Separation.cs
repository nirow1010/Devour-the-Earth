using System.Linq;
using UnityEngine;

public class Separation : MonoBehaviour
{
    public float spaceBetween;
    public float checkRadius;
    public float seporationSpeed;
    public float speed;

    public LayerMask checkLayer;
    public Collider2D[] nearMe;
    private float myRadius;

    void Start()
    {
        myRadius = GetColliderRadius(GetComponent<Collider2D>());
    }

    void Update()
    {
        nearMe = Physics2D.OverlapCircleAll(transform.position, checkRadius, checkLayer);

        foreach (Collider2D obj in nearMe)
        {
            if (obj != this.gameObject)
            {
                float otherRadius = GetColliderRadius(obj);

                float distance = Vector2.Distance(obj.transform.position, this.transform.position);
                float minDistance = myRadius + otherRadius + spaceBetween;

                if (distance <= minDistance)
                {
                    Vector2 direction = transform.position - obj.transform.position;
                    transform.Translate(direction * Time.deltaTime * seporationSpeed);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawWireSphere(transform.position, spaceBetween);
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
}
