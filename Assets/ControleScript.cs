using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleScript : MonoBehaviour
{
    [SerializeField] private Transform minionNodes;
    [SerializeField] private Transform collectedMinionNodes;

    private void Awake()
    {
        minionNodes = GameObject.Find("MinionsList").transform;
        collectedMinionNodes = GameObject.Find("CollectedList").transform;
    }
    public void loadScene(string scene) {

        setUpMinions(scene);
        SceneManager.LoadScene(scene);
    }

    private void setUpMinions(string scene) {
        GameObject[] allMinions = GameObject.FindGameObjectsWithTag("MinionNode");

        foreach (GameObject minion in allMinions)
        {
            if (minion.GetComponent<MinionData>().active)
            {
                minion.transform.SetParent(minionNodes);
            }
            else
            {
                minion.transform.SetParent(collectedMinionNodes);
            }

            if (scene == "Hub")
                minion.GetComponent<MinionData>().hubStart = true;
            if(scene == "Scene")
                minion.GetComponent<MinionData>().gameStart = true;
        }

    }

}
