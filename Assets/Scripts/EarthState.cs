using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthState : State
{
    [SerializeField] Color hitColor;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(ReactOnHit());
    }

    private IEnumerator ReactOnHit()
    {
        sr.color = hitColor;

        yield return new WaitForSeconds(0.2f);

        if (GetHealth() < 0)
        {
            Destroy(gameObject);
            //Application.Quit();
            SceneManager.LoadScene("Main Menu");
        } else if (GetHealth() < 40) // STAGE 5
        {
            
        } else if (GetHealth() < 60) // STAGE 4
        {
            
        } else if (GetHealth() < 80) // STAGE 3
        {
            
        } else if (GetHealth() < 95) // STAGE 2
        {
            
        }

        sr.color = Color.white;
    }

}
