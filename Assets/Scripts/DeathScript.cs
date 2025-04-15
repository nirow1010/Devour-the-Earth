using UnityEngine;

public class DeathScript : MonoBehaviour
{

    private EnemyState enemyScript;
    public GameObject minionType;
    public GameObject minionNode;
    void Start()
    {
        enemyScript = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void assimilate() 
    {
        GameObject minionN = Instantiate(minionNode);
        minionN.GetComponent<MinionData>().shipType = minionType;
        minionN.transform.SetParent(GameObject.Find("CollectedList").transform);
    }
}
