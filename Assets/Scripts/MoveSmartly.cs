using UnityEngine;

public class MoveSmartly : MonoBehaviour
{
    [SerializeField] Transform obj;
    [SerializeField] Transform bar;

    // Update is called once per frame
    void Update()
    {
        bar.Translate(obj.position.x - bar.position.x, obj.position.y - bar.position.y + 2, obj.position.z - bar.position.z);
    }
}
