using UnityEngine;

public class MoveSmartlyEarth : MonoBehaviour
{
    [SerializeField] Transform obj;

    // Update is called once per frame
    void Update()
    {
        if (obj == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(obj.position.x - transform.position.x, obj.position.y - transform.position.y - 12, 0);
        }
    }
}
