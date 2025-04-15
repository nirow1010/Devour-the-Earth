using UnityEngine;

public class AssimilatedKamikazeSkill : KamikazeSkill
{
    protected override bool IsSkillUseTriggered()
    {
        return Input.GetKeyDown(KeyCode.K); // placeholder
    }
}
