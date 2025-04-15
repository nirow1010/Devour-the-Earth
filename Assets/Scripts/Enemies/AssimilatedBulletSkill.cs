using UnityEngine;

public class AssimilatedBulletSkill : BulletSkill
{
    [SerializeField] AssimilationBullet bulletPrefab;

    public override void UseSkill()
    {
        AssimilationBullet bullet = Instantiate(bulletPrefab, bulletFirePoint.position, transform.rotation);

        bullet.SetDamage(GetDamage());
        bullet.SetSpeed(bulletSpeed);

        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }
}
