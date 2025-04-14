using UnityEngine;

public class AssimilationBullet : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;

    public int damage = 1;
    public float speed = 5;
    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Camera"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyState enemy = collider.gameObject.GetComponent<EnemyState>();
        EarthState earth = collider.gameObject.GetComponent<EarthState>();

        // Placeholder for collision audio
        audioSource.clip = collisionAudio;
        audioSource.Play();
        
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (earth != null)
        {
            earth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
