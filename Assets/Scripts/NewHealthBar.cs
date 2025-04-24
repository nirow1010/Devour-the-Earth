using UnityEngine;
using UnityEngine.UI;

public class NewHealthBar : MonoBehaviour
{
    public State healthScript;
    private float relativeHealth;
    private Image healthBar;
    void Start()
    {
        healthBar = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        relativeHealth = healthScript.GetHealth() / healthScript.startingHealth;
        healthBar.fillAmount = relativeHealth;
    }
}
