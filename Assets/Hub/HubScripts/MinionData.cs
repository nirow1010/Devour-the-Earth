using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinionData : MonoBehaviour
{
    public GameObject shipType;
    public Vector2 hubLocation;
    public Vector2 gameLocation;
    public bool active;

    public bool hubStart;
    public bool gameStart;

    [SerializeField] private GameObject StickerPrefab;
    [SerializeField] private Transform grid;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject player;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            hubStart = true;
        }
        else if (SceneManager.GetActiveScene().name == "Scene")
        {
            gameStart = true;
        }
    }
    private void Update()
    {
        if (hubStart && SceneManager.GetActiveScene().name == "Hub")
        {
            hubStart = false;
            hubSetup();
        }
        else if (gameStart && SceneManager.GetActiveScene().name == "Scene")
        {
            gameStart = false;
            gameSetup();
        }
    }

    private void hubSetup() 
    {
            grid = GameObject.Find("Canvas").transform.Find("Ship Menu").Find("Viewport").Find("Content");
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            GameObject sticker = Instantiate(StickerPrefab);
            sticker.transform.SetParent(canvas.transform, true);
            sticker.GetComponent<RectTransform>().localScale = Vector3.one;
            sticker.GetComponentInChildren<UnityEngine.UI.Image>().sprite = shipType.GetComponent<SpriteRenderer>().sprite;


            if (active)
            {
                //Debug.Log("Hello");
                sticker.GetComponent<RectTransform>().anchorMax = new Vector2(0,1);
                sticker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                sticker.GetComponent<RectTransform>().anchoredPosition = GetComponent<MinionData>().hubLocation;
            }
            else
            {
                sticker.transform.SetParent(grid);
            }
            transform.SetParent(sticker.transform);
    }
    private void gameSetup() 
    {
            player = GameObject.Find("Player");

            if (active)
            {
                transform.position = gameLocation;
                GameObject minion = Instantiate(shipType);
                minion.transform.SetParent(player.transform);
                this.transform.SetParent(minion.transform);
                minion.transform.position = this.transform.position;
            }
    }

}
