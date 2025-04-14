using UnityEngine;

public abstract class EnemyChargeSkill : EnemySkill
{
    private float minChargeTime;
    private float maxChargeTime;
    private float chargeDuration;
    private float lastShootTime;

    protected void Initializer(float baseDamage, float cooldown, float minChargeTime, float maxChargeTime)
    {
        SetBaseDamage(baseDamage);
        SetCooldown(cooldown);
        SetChargeInterval(minChargeTime, maxChargeTime);
        lastShootTime = -cooldown;
    }

    public void SetChargeInterval(float min, float max)
    {
        minChargeTime = min;
        maxChargeTime = max;
    }

    public float GetMinChargeTime()
    {
        return minChargeTime;
    }

    public float GetMaxChargeTime()
    {
        return maxChargeTime;
    }

    public float GetChargeDuration()
    {
        return chargeDuration;
    }

    public override bool IsSkillConditionMet()
    {
        bool isMet = false;

        if (Input.GetKeyUp(KeyCode.V))
        {
            isMet = chargeDuration >= minChargeTime;
            if (isMet) lastShootTime = Time.time;
            chargeDuration = 0;
        }
        else if (Input.GetKey(KeyCode.V) && Time.time - lastShootTime >= GetCooldown())
        {
            Debug.Log(chargeDuration);
            chargeDuration += Time.deltaTime;

            if (chargeDuration > maxChargeTime)
            {
                chargeDuration = maxChargeTime;
            }
        }

        return isMet;
    }
}
