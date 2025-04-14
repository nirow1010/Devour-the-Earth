using System.Linq;
using UnityEngine;

public class Seporation : MonoBehaviour
{
    public float spaceBetween;
    public float checkRadius;
    public float seporationSpeed;

    public GameObject goal;
    public float speed;
    public bool isenemy;


    public LayerMask checkLayer;
    public Collider2D[] neerMe;
    private float myRadius;
    void Start()
    {
        myRadius = GetColliderRadius(GetComponent<Collider2D>());

        if (isenemy)
            goal = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        neerMe = Physics2D.OverlapCircleAll(transform.position, checkRadius, checkLayer);
            foreach (Collider2D obj in neerMe)
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
