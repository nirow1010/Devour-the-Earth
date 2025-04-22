//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinionData : MonoBehaviour
{
    public GameObject shipType;
    public Vector2 hubLocation;
    public Vector2 gameLocation;
    public Quaternion minionAngle;
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
            sticker.GetComponent<RectTransform>().localScale = new Vector2(2,2);
            //sticker.GetComponentInChildren<UnityEngine.UI.Image>().sprite = shipType.GetComponent<SpriteRenderer>().sprite;
            sticker.GetComponentInChildren<UnityEngine.UI.Image>().sprite = shipType.GetComponentInChildren<SpriteRenderer>().sprite;

        if (active)
        {
            //Debug.Log("Hello");
            sticker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            sticker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            sticker.GetComponent<RectTransform>().anchoredPosition = GetComponent<MinionData>().hubLocation;
            sticker.GetComponent<RectTransform>().rotation = minionAngle;
        }
        else
        {
            sticker.transform.SetParent(grid);
        }
        transform.SetParent(sticker.transform);
        this.transform.localPosition = Vector2.zero;

    }
    private void gameSetup() 
    {
            player = GameObject.Find("Player");

        if (active)
        {
            GameObject minion = Instantiate(shipType);
            this.transform.SetParent(player.transform);
            minion.transform.localScale = new Vector2(2, 2);
            minion.transform.position = player.transform.position;
            minion.GetComponentInChildren<BasicPathfinding>().SetGoal(gameObject);

            minion.transform.localPosition = gameLocation;
            this.transform.localPosition = gameLocation;

            minion.transform.localRotation = minionAngle;
        }
    }

    public Vector2 hubToGameLocation() 
    {
        GameObject ship = GameObject.Find("Centerpiece");
        Vector2 midpoint = ship.GetComponent<RectTransform>().TransformPoint(ship.GetComponent<RectTransform>().rect.center);
        Vector2 minionPoint = this.GetComponentInParent<RectTransform>().TransformPoint(this.GetComponentInParent<RectTransform>().rect.center);
        
        //Debug.Log(midpoint);

        Vector2 offset = minionPoint - midpoint;
        //Debug.Log(offset);
        return offset;
    }

}
