using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneSetUp : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Start()
    {
        GlobalStats.enemiesOnSkreen = 9;
        foreach (Assimilation.ShipData shipData in Assimilation.minionList)
        {
            GameObject homeNode = new GameObject();
            homeNode.transform.SetParent(player.transform);
            
            GameObject minion = Instantiate(shipData.shipType, player.transform.position, player.transform.rotation);
            minion.transform.localScale = new Vector2(2, 2);
            minion.transform.position = player.transform.position;
            minion.GetComponentInChildren<BasicPathfinding>().SetGoal(homeNode);
            minion.GetComponentInChildren<MinionState>().shipData = shipData;

            minion.transform.localPosition = shipData.gameLocation;
            homeNode.transform.localPosition = shipData.gameLocation;
            //minion.transform.localRotation = shipData.minionAngle;
        }
    }

    //private void gameSetup()
    //{
    //    player = GameObject.Find("Player");
    //    if (active)
    //    {
    //        GameObject minion = Instantiate(shipType);
    //        this.transform.SetParent(player.transform);
    //        minion.transform.localScale = new Vector2(2, 2);
    //        minion.transform.position = player.transform.position;
    //        minion.GetComponentInChildren<BasicPathfinding>().SetGoal(gameObject);

    //        minion.transform.localPosition = gameLocation;
    //        this.transform.localPosition = gameLocation;

    //        minion.transform.localRotation = minionAngle;
    //    }
    //}
}
