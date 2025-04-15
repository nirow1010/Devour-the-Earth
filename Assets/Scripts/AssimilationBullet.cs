using UnityEngine;

public class AssimilationBullet : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;
    private AudioSource audioSource;
    private float damage = 1;
    private float speed = 5;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Placeholder for collision audio
        audioSource.clip = collisionAudio;
        audioSource.Play();

        State state = collider.gameObject.GetComponent<State>();

        if (state != null)
        {
            state.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
