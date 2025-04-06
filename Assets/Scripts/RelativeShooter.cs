using UnityEngine;

public class RelativeShooter : MonoBehaviour
{
    [SerializeField] AssimilationBullet bulletPrefab;
    
    public float delayTime = 0.1f;
    public float bulletSpeed = 5;

    private bool canShoot = true;
    private float lastShootTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            canShoot = Time.time - lastShootTime >= delayTime;
        }

        if (canShoot && Input.GetKey(KeyCode.Space))
        {
            Vector3 bulletPos = transform.position + transform.up;
            Quaternion bulletRot = transform.rotation;

            AssimilationBullet bullet = Instantiate(bulletPrefab, bulletPos, bulletRot);
            bullet.SetSpeed(bulletSpeed);

            lastShootTime = Time.time;
            canShoot = false;
        }
    }
}
