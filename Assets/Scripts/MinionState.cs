using System.Collections;
using UnityEngine;

public class MinionState : State
{
    [SerializeField] Color hitColor;
    private SpriteRenderer sr;

    public Assimilation.ShipData shipData;

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

        if (!IsAlive())
        {
            if (shipData != null)
                Assimilation.minionList.Remove(shipData);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }

        sr.color = Color.white;
    }
}
