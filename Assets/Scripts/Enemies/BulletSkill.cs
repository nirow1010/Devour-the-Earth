using UnityEngine;

public class BulletSkill : EnemyInstantSkill
{
    public Transform bulletFirePoint;
    public float bulletSpeed = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(0.5f, 1.5f);
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }
}
