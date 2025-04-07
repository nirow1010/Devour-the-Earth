using System.Threading;
using UnityEngine;

public class HubControlScript : MonoBehaviour
{
    [SerializeField] private Transform minionNodes;
    [SerializeField] private Transform collectedMinionNodes;
    [SerializeField] private GameObject StickerPrefab;
    [SerializeField] private Transform grid;
    void Start()
    {
        generateHub();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateHub() {
        //foreach (Transform minion in collectedMinionNodes)
        //{
        //    GameObject sticker = Instantiate(StickerPrefab);
        //    sticker.transform.SetParent(grid);
        //    sticker.GetComponent<RectTransform>().localScale = Vector3.one;
        //    minion.transform.SetParent(sticker.transform);
        //}
        //foreach (Transform minion in minionNodes)
        //{
        //    GameObject sticker = Instantiate(StickerPrefab);
        //    sticker.GetComponent<RectTransform>().anchoredPosition = minion.GetComponent<MinionData>().hubLocation;
        //    minion.transform.SetParent(sticker.transform);
        //    sticker.GetComponent<RectTransform>().localScale = Vector3.one;
        //}
    }
}
