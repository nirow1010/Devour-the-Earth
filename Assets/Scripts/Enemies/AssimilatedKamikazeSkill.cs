using UnityEngine;

[RequireComponent (typeof(BasicPathfinding)), RequireComponent (typeof(Seporation))]
public class AssimilatedKamikazeSkill : KamikazeSkill
{
    private BasicPathfinding enemyPathfinding;
    private Seporation seporation;

    protected override void Start()
    {
        enemyPathfinding = GetComponent<BasicPathfinding>();
        seporation = GetComponent<Seporation>();
        base.Start();
    }

    public override void UseSkill()
    {
        enemyPathfinding.enabled = false;
        seporation.enabled = false;
        base.UseSkill();
    }

    protected override bool IsSkillUseTriggered()
    {
        return Input.GetKeyDown(KeyCode.K); // placeholder
    }
}
