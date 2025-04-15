using UnityEngine;

[RequireComponent (typeof(EnemyPathfinding))]
public class AIBulletSkill : BulletSkill
{
    private EnemyPathfinding enemyPathfinding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    protected override bool IsSkillUseTriggered()
    {
        return enemyPathfinding.IsOnFight() && base.IsSkillUseTriggered();
    }
}
