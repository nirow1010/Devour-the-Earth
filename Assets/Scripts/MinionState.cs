using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MinionState : State, Stunnable
{
    [SerializeField] Color hitColor;
    public Assimilation.ShipData shipData;

    private SpriteRenderer sr;
    private EnemySkill skill;
    private bool isStunned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        skill = GetComponent<EnemySkill>();
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    public void GetStunned(float time)
    {
        StopCoroutine(StunFor(time));
        StartCoroutine(StunFor(time));
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

    private IEnumerator StunFor(float time)
    {
        isStunned = true;
        skill.enabled = false;

        yield return new WaitForSeconds(time);

        skill.enabled = true;

        isStunned = false;
    }
}
