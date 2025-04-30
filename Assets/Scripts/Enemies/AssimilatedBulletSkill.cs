using UnityEngine;

public class AssimilatedBulletSkill : BulletSkill
{
    [SerializeField] Projectile bulletPrefab;

    public override void UseSkill()
    {
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
