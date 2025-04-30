using UnityEngine;

public class AssimilationBullet : Projectile
{
    [SerializeField] private AudioClip collisionAudio;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position + transform.up * 0.5f, transform.rotation);

        if (collisionAudio != null)
        {
            audioSource.clip = collisionAudio;
            audioSource.Play();
        }

        State state = collider.gameObject.GetComponent<State>();

        if (state != null)
        {
            state.TakeDamage(GetDamage());
        }

        Destroy(gameObject);
    }
}
