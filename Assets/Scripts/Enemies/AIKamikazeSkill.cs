using UnityEngine;

[RequireComponent (typeof(EnemyPathfinding))]
public class AIKamikazeSkill : KamikazeSkill
{
    public float kamikazeCounter;
    private EnemyPathfinding enemyPathfinding;
    private float startTime;

    protected override void Start()
    {
        base.Start();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        startTime = Time.time;
    }

    public override void UseSkill()
    {
        enemyPathfinding.enabled = false;
        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        if (enemyPathfinding.IsOnFight())
        {
            return Time.time - startTime >= kamikazeCounter;
        }
        else
        {
            startTime = Time.time;
            return false;
        }
    }
}
