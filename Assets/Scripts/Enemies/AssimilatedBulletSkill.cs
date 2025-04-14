using UnityEngine;

public class AssimilatedBulletSkill : BulletSkill
{
    protected override bool IsSkillUseTriggered()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }
}
