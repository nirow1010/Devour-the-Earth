using UnityEngine;

public abstract class EnemySkill : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownRandModifier = 0f;
    
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

    public float GetCooldownModifier()
    {
        return cooldownRandModifier;
    }

    public void SetCooldownModifier(float modifier)
    {
        this.cooldownRandModifier = modifier;
    }

    public abstract void UseSkill();

    public abstract bool IsSkillConditionMet();
}
