using UnityEngine;

public class HealthBar : MonoBehaviour
{
    
    private float health;
    private float maxHealth;
    private float healthNormalize;
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
        health = script.GetHealth();
        sr.sprite = s0;
    }

    // Update is called once per frame
    void Update()
    {
        health = script.GetHealth();
        maxHealth = script.startingHealth;
        healthNormalize = health / maxHealth * 100;
        Debug.Log("health is: " + healthNormalize);
        if(s5 == null) {
            if(healthNormalize < 20) {
                sr.sprite = s4;
            } else if(healthNormalize < 40) {
                sr.sprite = s3;
            } else if(healthNormalize < 60) {
                sr.sprite = s2;
            } else if(healthNormalize < 80) {
                sr.sprite = s1;
            }
        } else {
            if(healthNormalize < 14) {
                sr.sprite = s6;
            } else if(healthNormalize < 28) {
                sr.sprite = s5;
            } else if(healthNormalize < 42) {
                sr.sprite = s4;
            } else if(healthNormalize < 56) {
                sr.sprite = s3;
            } else if(healthNormalize < 70) {
                sr.sprite = s2;
            } else if(healthNormalize < 85) {
                sr.sprite = s1;
            }
        }
    }
}
