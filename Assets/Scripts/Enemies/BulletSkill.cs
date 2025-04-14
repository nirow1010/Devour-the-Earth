using UnityEngine;

public class BulletSkill : EnemyInstantSkill
{
    [SerializeField] Transform bulletFirePoint;
    [SerializeField] Bullet bulletPrefab;
    public float bulletSpeed = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initializer(0.5f, 0.5f);
    }

    public override void UseSkill()
    {
        Bullet bullet = Instantiate(bulletPrefab);

        bullet.transform.position = bulletFirePoint.position;
        bullet.transform.localEulerAngles = transform.up;
        bullet.SetDamage(GetDamage());
        bullet.SetSpeed(bulletSpeed);

        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }
}
