using System.Collections;
using UnityEngine;

public class KamikazeSkill : EnemyInstantSkill
{
    public float maxTravelSpeed = 15;
    public float accelRate = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initializer(100f, 0f);
    }

    public override void UseSkill()
    {
        StartCoroutine(initiateKamikaze());
    }

    protected IEnumerator initiateKamikaze()
    {
        yield return null;
        Destroy(gameObject);
    }

    protected override bool IsSkillUseTriggered()
    {
        return true; // placeholder
    }
}
