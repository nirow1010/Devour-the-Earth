using UnityEngine;

public abstract class EnemyChargeSkill : EnemySkill
{
    [SerializeField] GameObject chargeEffect;
    [SerializeField] Transform firePoint;

    private float minChargeTime;
    private float maxChargeTime;
    private float chargeTimer;
    private float chargeDuration;
    private float lastShootTime;

    private GameObject tempChargeEffect;

    protected void Initializer(float baseDamage, float cooldown, float minChargeTime, float maxChargeTime)
    {
        SetBaseDamage(baseDamage);
        SetCooldown(cooldown);
        SetChargeInterval(minChargeTime, maxChargeTime);
        lastShootTime = -cooldown;
    }

    protected GameObject GetChargeEffect()
    {
        return chargeEffect;
    }

    protected Transform GetFirePoint()
    {
        return firePoint;
    }

    protected void SetChargeInterval(float min, float max)
    {
        minChargeTime = min;
        maxChargeTime = max;
    }

    protected float GetMinChargeTime()
    {
        return minChargeTime;
    }

    protected float GetMaxChargeTime()
    {
        return maxChargeTime;
    }

    protected float GetChargeDuration()
    {
        return chargeDuration;
    }

    protected void SetCharageDuration(float chargeDuration)
    {
        this.chargeDuration = chargeDuration;
    }

    public override void UseSkill()
    {
        lastShootTime = Time.time;
        chargeDuration = 0;
    }

    public override bool IsSkillConditionMet()
    {
        bool isMet = false;

        if (Time.time - lastShootTime >= GetCooldown())
        {
            if (IsSkillChargeReleased())
            {
                Destroy(tempChargeEffect);
                isMet = chargeTimer >= minChargeTime;
                chargeTimer = 0;
            }
            else if (IsSkillChargeTriggered())
            {
                if (tempChargeEffect == null)
                {
                    tempChargeEffect = Instantiate(chargeEffect);

                    SelfDestructiveFollowingEffect effect = tempChargeEffect.GetComponent<SelfDestructiveFollowingEffect>();

                    if (effect != null)
                    {
                        effect.SetFollow(firePoint);
                    }
                }

                chargeTimer += Time.deltaTime;

                if (chargeTimer > maxChargeTime)
                {
                    chargeTimer = maxChargeTime;
                }

                chargeDuration = chargeTimer;
            }
            else
            {
                Destroy(tempChargeEffect);
                chargeTimer = 0;
            }
        }

        return isMet;
    }

    // Override this to make AI start charging
    protected abstract bool IsSkillChargeTriggered();

    // Override this to make AI shoot
    protected abstract bool IsSkillChargeReleased();
}
