using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] State script;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] UnityEngine.Sprite s0;
    [SerializeField] UnityEngine.Sprite s1;
    [SerializeField] UnityEngine.Sprite s2;
    [SerializeField] UnityEngine.Sprite s3;
    [SerializeField] UnityEngine.Sprite s4;
    [SerializeField] UnityEngine.Sprite s5;
    [SerializeField] UnityEngine.Sprite s6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.sprite = s0;
    }

    // Update is called once per frame
    void Update()
    {
        float health = script.GetHealth();
        float maxHealth = script.startingHealth;
        float healthNormalize = health / maxHealth * 100;

        if (s5 == null) {
            if (healthNormalize <= 0) {
                sr.sprite = s4;
            } else if(healthNormalize <= 25) {
                sr.sprite = s3;
            } else if(healthNormalize <= 50) {
                sr.sprite = s2;
            } else if(healthNormalize <= 75) {
                sr.sprite = s1;
            }
        } else {
            if (healthNormalize <= 0) {
                sr.sprite = s6;
            } else if(healthNormalize <= 50 / 3) {
                sr.sprite = s5;
            } else if(healthNormalize <= 100 / 3) {
                sr.sprite = s4;
            } else if(healthNormalize <= 50) {
                sr.sprite = s3;
            } else if(healthNormalize <= 200 / 3) {
                sr.sprite = s2;
            } else if(healthNormalize <= 250 / 3) {
                sr.sprite = s1;
            }
        }
    }
}
