using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class RelativeMovement : MonoBehaviour
{
    public float maxSpeed = 5;
    public float accelRate = 1.2f;
    public float decelRate = 1.2f;
    private Vector2 velocity;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocity = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 targetVelocity = new Vector2(inputX * maxSpeed, inputY * maxSpeed);
        velocity += (targetVelocity - velocity) * ((inputX == 0 && inputY == 0) ? decelRate : accelRate);

        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
