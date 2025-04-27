using UnityEngine;

[RequireComponent (typeof(BasicPathfinding)), RequireComponent (typeof(Separation))]
public class AssimilatedKamikazeSkill : KamikazeSkill
{
    private BasicPathfinding enemyPathfinding;
    private Separation separation;

    protected override void Start()
    {
        enemyPathfinding = GetComponent<BasicPathfinding>();
        separation = GetComponent<Separation>();
        base.Start();
    }

    public override void UseSkill()
    {
        enemyPathfinding.enabled = false;
        separation.enabled = false;
        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return Input.GetKeyDown(KeyCode.Alpha1); // placeholder
    }
}
