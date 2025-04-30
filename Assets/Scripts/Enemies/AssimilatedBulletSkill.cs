using UnityEngine;

public class AssimilatedBulletSkill : BulletSkill
{
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] private AudioClip collisionAudio;
    private AudioSource audioSource;

    public override void UseSkill()
    {
        // bullet shoot sound
        if (collisionAudio != null)
        {
            audioSource.clip = collisionAudio;
            audioSource.Play();
        }
        
        Projectile bullet = Instantiate(bulletPrefab, bulletFirePoint.position, transform.rotation);

        bullet.SetDamage(GetDamage());
        bullet.SetSpeed(bulletSpeed);

        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return Input.GetMouseButton(0);
    }
}
