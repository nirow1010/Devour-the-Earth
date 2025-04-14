using UnityEngine;

public class EarthOrbits : MonoBehaviour
{
    public float desplayRadius;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, desplayRadius);
    }
}
