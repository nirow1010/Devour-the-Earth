using System.ComponentModel.Design;
using UnityEngine;

public abstract class EnemyInstantSkill : EnemySkill
{
    private float lastShootTime;

    protected virtual void Start()
    {
        
    }

    protected void Initializer(float baseDamage, float cooldown)
    {
        SetBaseDamage(baseDamage);
        SetCooldown(cooldown);
        lastShootTime = -cooldown;
    }

    public override void UseSkill()
    {
        lastShootTime = Time.time;
    }

    public override bool IsSkillConditionMet()
    {
        return Input.GetKey(KeyCode.K) && Time.time - lastShootTime >= GetCooldown();
    }
}
