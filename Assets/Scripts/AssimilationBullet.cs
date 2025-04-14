using UnityEngine;

public class AssimilationBullet : Bullet
{
    [SerializeField] private AudioClip collisionAudio;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // Placeholder for collision audio
        audioSource.clip = collisionAudio;
        audioSource.Play();

        base.OnTriggerEnter2D(collider);
    }
}
