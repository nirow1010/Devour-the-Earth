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
        relitiveHealth = (earthState.GetHealth() / earthState.startingHealth);
        int phase = (int)((1f - relitiveHealth) * (earthPhases.Length - 1));

        if (phase > phaseCount)
        {
            earth.sprite = earthPhases[phase];

            if (phase < earthPhases.Length - 1)
                CamShake.Instance.shake(15f, 1.5f);

            phaseCount = phase;
        }
    }
}
