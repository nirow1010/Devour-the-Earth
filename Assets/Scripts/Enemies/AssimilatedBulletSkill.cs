using UnityEngine;

public class AssimilatedBulletSkill : BulletSkill
{
    [SerializeField] private AudioClip launchAudio;
    [SerializeField] Projectile bulletPrefab;
    private AudioSource audioSource;

    protected override void Start() {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    public override void UseSkill()
    {
        if (audioSource != null && launchAudio != null)
        {
            audioSource.clip = launchAudio;
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
