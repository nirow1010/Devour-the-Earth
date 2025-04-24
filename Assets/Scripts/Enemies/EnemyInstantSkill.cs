using System.ComponentModel.Design;
using UnityEngine;

public abstract class EnemyInstantSkill : EnemySkill
{
    private float lastShootTime;

    private float randNum;
    protected void Initializer(float baseDamage, float cooldown)
    {
        SetBaseDamage(baseDamage);
        SetCooldown(cooldown);
        lastShootTime = -cooldown;
    }

    protected void Initializer(float baseDamage, float cooldown, float modifier)
    {
        SetBaseDamage(baseDamage);
        SetCooldown(cooldown);
        SetCooldownModifier(modifier);
        lastShootTime = -cooldown;
    }
    public override void UseSkill()
    {
        randNum = UnityEngine.Random.Range(-GetCooldownModifier(), GetCooldownModifier());
        lastShootTime = Time.time + randNum;
    }

    public override bool IsSkillConditionMet()
    {
        return IsSkillUseTriggered() && Time.time - lastShootTime >= GetCooldown();
    }
    
    // Override this to make AI use skill
    protected abstract bool IsSkillUseTriggered();
}
