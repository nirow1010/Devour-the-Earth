using UnityEngine;

public class AssimilatedLaserBeamSkill : LaserBeamSkill
{
    protected override bool IsSkillChargeTriggered()
    {
        return Input.GetKey(KeyCode.V);
    }

    // Connect you AI skill charge release to here
    protected override bool IsSkillChargeReleased()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }
}
