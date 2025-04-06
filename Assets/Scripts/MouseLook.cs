using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float rotSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono) - transform.position;
        Quaternion direction = Quaternion.Euler(0, 0, Mathf.Atan2(-lookDir.x, lookDir.y) * 360 / (2 * Mathf.PI));
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
    }
}
