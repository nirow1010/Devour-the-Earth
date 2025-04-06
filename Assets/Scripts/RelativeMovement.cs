using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class RelativeMovement : MonoBehaviour
{
    public float maxSpeed = 5;
    public float accelRate = 1.2f;
    public float decelRate = 1.2f;

    private Vector2 moveDir;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDir = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();

        Vector2 moveForce = (moveDir * maxSpeed - rb.linearVelocity) * (moveDir.magnitude > 0 ? accelRate : decelRate);
        rb.AddForce(moveForce, ForceMode2D.Force);
    }
}
