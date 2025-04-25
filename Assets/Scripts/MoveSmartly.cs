using UnityEngine;

public class MoveSmartly : MonoBehaviour
{
    [SerializeField] Transform obj;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(obj.position.x - transform.position.x, obj.position.y - transform.position.y + 2, obj.position.z - transform.position.z);
    }
}
