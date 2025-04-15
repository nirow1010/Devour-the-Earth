using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPathfinding)), RequireComponent(typeof(Rigidbody2D))]
public class AILaserBeamSkill : LaserBeamSkill
{
    private EnemyPathfinding enemyPathfinding;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void UseSkill()
    {
        enemyPathfinding.enabled = false;
        rb.linearVelocity = Vector3.zero;
        base.UseSkill();
    }

    protected override IEnumerator DrawFadingLaser(Vector2 startPos, Vector2 endPos, float thickness)
    {
        yield return base.DrawFadingLaser(startPos, endPos, thickness);
        enemyPathfinding.enabled = true;
    }

    protected override bool IsSkillChargeTriggered()
    {
        return enemyPathfinding.IsOnFight() && base.IsSkillChargeTriggered();
    }
}
