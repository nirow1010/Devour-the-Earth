using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPathfinding))]
public class AILaserBeamSkill : LaserBeamSkill
{
    private EnemyPathfinding enemyPathfinding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    public override void UseSkill()
    {
        enemyPathfinding.enabled = false;
        base.UseSkill();
    }

    protected override IEnumerator DrawFadingLaser(Vector2 startPos, Vector2 endPos, float thickness)
    {
        yield return base.DrawFadingLaser(startPos, endPos, thickness);
        enemyPathfinding.enabled = true;
    }
}
