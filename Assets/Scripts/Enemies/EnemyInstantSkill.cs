using System.ComponentModel.Design;
using UnityEngine;

public abstract class EnemyInstantSkill : EnemySkill
{
    private float lastShootTime;

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
        return IsSkillUseTriggered() && Time.time - lastShootTime >= GetCooldown();
    }
    
    // Override this to make AI use skill
    protected abstract bool IsSkillUseTriggered();
}
