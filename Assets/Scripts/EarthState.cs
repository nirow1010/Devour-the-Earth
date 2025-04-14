using System.Collections;
using UnityEngine;

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
            Application.Quit();
        } else if (GetHealth() < 1)
        {
            //STAGE 5
        } else if (GetHealth() < 2)
        {
            //STAGE 4
        } else if (GetHealth() < 3)
        {
            //STAGE 3
        } else
        {
            //STAGE 2
        }

        sr.color = Color.white;
    }

}
