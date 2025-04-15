using UnityEngine;

public class BulletSkill : EnemyInstantSkill
{
    [SerializeField] Transform bulletFirePoint;
    [SerializeField] Bullet bulletPrefab;
    public float bulletSpeed = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(0.5f, 1.5f);
    }

    public override void UseSkill()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletFirePoint.position, transform.rotation);

        bullet.SetDamage(GetDamage());
        bullet.SetSpeed(bulletSpeed);

        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }
}
