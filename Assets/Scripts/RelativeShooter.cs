using UnityEngine;

public class RelativeShooter : MonoBehaviour
{
    [SerializeField] private AudioClip launchAudio;
    private AudioSource audioSource;

    [SerializeField] AssimilationBullet bulletPrefab;
    
    public float delayTime = 0.1f;
    public float bulletSpeed = 5;
    public float bulletSummonOffset = 1.5f;

    private bool canShoot = true;
    private float lastShootTime = 0;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            canShoot = Time.time - lastShootTime >= delayTime;
        }

        if (canShoot && Input.GetMouseButton(0))
        {
            if (audioSource != null && launchAudio != null)
            {
                audioSource.clip = launchAudio;
                audioSource.Play();
            }

            Vector3 bulletPos = transform.position + transform.up * bulletSummonOffset;
            Quaternion bulletRot = transform.rotation;

            AssimilationBullet bullet = Instantiate(bulletPrefab, bulletPos, bulletRot);
            bullet.SetSpeed(bulletSpeed);

            lastShootTime = Time.time;
            canShoot = false;
        }
    }
}
