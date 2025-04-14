using UnityEngine;

public abstract class EnemySkill : MonoBehaviour
{
    private float baseDamage;
    private float cooldown;

    protected virtual void Update()
    {
        if (IsSkillConditionMet())
        {
            UseSkill();
        }
    }

    public virtual float GetDamage()
    {
        return baseDamage;
    }

    public void SetBaseDamage(float baseDamage)
    {
        this.baseDamage = baseDamage;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public void SetCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public abstract void UseSkill();

    public abstract bool IsSkillConditionMet();
}
