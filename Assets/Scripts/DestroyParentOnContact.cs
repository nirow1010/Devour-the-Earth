using UnityEngine;

public class DestroyParentOnContact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(transform.parent.gameObject);
    }
}
