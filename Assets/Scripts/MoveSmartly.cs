using UnityEngine;

public class MoveSmartly : MonoBehaviour
{
    [SerializeField] Transform obj;

    // Update is called once per frame
    void LateUpdate()
    {
        if (obj == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(obj.transform.position.x - transform.position.x,
                obj.transform.position.y - transform.position.y + 2,
                obj.transform.position.z - transform.position.z);
        }
    }
}
