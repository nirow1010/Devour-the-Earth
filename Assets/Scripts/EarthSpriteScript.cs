using UnityEngine;

public class EarthSpriteScript : MonoBehaviour
{
    public UnityEngine.Sprite[] earthPhases;
    private SpriteRenderer earth;
    private EarthState earthState;
    private float relitiveHealth;
    private int phaseCount = 0;
    void Start()
    {
        earth = this.gameObject.GetComponent<SpriteRenderer>();
        earthState = gameObject.GetComponent<EarthState>();
        relitiveHealth = earthState.startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        relitiveHealth = (earthState.GetHealth() / earthState.startingHealth) * 100;
        float nextPhaseHealth = (earthState.startingHealth / earthPhases.Length) * (earthPhases.Length - phaseCount);
        if (relitiveHealth <= nextPhaseHealth && relitiveHealth > 0)
        {
            earth.sprite = earthPhases[phaseCount];

            if(phaseCount > 0)
            CamShake.Instance.shake(15f,1.5f);

            phaseCount++;
        }
    }
}
