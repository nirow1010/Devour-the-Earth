using NUnit.Framework.Constraints;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeamSkill : EnemyChargeSkill
{
    public LayerMask laserHitLayers;
    // need charging animation

    public float laserDistance = 10;
    public float laserEmergeTime = 0.15f;
    public float laserStayTime = 1;
    public float laserFadeTime = 0.5f;

    public float baseLaserWidth = 1;
    public float laserSizeIncreaseRatio = 1;
    public float laserDamageIncreaseRatio = 1;

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initializer(GetDamage(), GetCooldown(), 2, 4);
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override float GetDamage()
    {
        return base.GetDamage() * (1 + laserDamageIncreaseRatio * (GetChargeDuration() - GetMinChargeTime()));
    }

    public override void UseSkill()
    {
        float laserWidth = baseLaserWidth * (1 + laserSizeIncreaseRatio * (GetChargeDuration() - GetMinChargeTime()));

        StartCoroutine(DrawFadingLaser(GetFirePoint().position + transform.up * 0.5f, GetFirePoint().position + transform.up * laserDistance, laserWidth));

        RaycastHit2D[] hits = Physics2D.CircleCastAll(GetFirePoint().position, laserWidth / 2, transform.up, laserDistance, laserHitLayers);

        foreach (RaycastHit2D hit in hits)
        {
            State state = hit.transform.gameObject.GetComponent<State>();

            if (state != null)
            {
                state.TakeDamage(GetDamage());
            }
        }

        base.UseSkill();
    }

    protected virtual IEnumerator DrawFadingLaser(Vector2 startPos, Vector2 endPos, float thickness)
    {
        Draw2DRay(startPos + (Vector2) transform.up * thickness / 4, endPos);
        
        float laserTime = 0;

        while (laserTime < laserEmergeTime)
        {
            float laserWidth = thickness * laserTime / laserEmergeTime;
            lineRenderer.startWidth = laserWidth;
            laserTime += Time.deltaTime;
            yield return null;
        }
        
        yield return new WaitForSeconds(laserStayTime);

        laserTime = laserFadeTime;

        while (laserTime > 0)
        {
            float laserWidth = thickness * laserTime / laserFadeTime;
            lineRenderer.startWidth = laserWidth;
            laserTime -= Time.deltaTime;
            yield return null;
        }

        Draw2DRay(new Vector2(0, 0), new Vector2(0, 0));
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    // Connect your AI skill chrage trigger to here
    protected override bool IsSkillChargeTriggered()
    {
        return true; // placeholder
    }

    // Connect you AI skill charge release to here
    protected override bool IsSkillChargeReleased()
    {
        return GetChargeDuration() >= GetMinChargeTime();
    }
}
