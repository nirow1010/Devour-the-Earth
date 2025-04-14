using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeamSkill : EnemyChargeSkill
{
    [SerializeField] Transform laserFirePoint;
    public float laserDistance = 100;
    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        Initializer(5, 4.5f, 3, 6);
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override float GetDamage()
    {
        return base.GetDamage() * GetChargeDuration() / 3.0f;
    }

    public override void UseSkill()
    {
        Draw2DRay(laserFirePoint.position, transform.up * laserDistance);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(laserFirePoint.position, GetChargeDuration() / 6.0f, transform.up, laserDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                // deal damage
            }
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
