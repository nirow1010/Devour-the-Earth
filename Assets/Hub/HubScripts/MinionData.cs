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
            sticker.GetComponent<RectTransform>().localScale = Vector3.one;
            sticker.GetComponentInChildren<UnityEngine.UI.Image>().sprite = shipType.GetComponent<SpriteRenderer>().sprite;

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
            this.transform.SetParent(minion.transform);
            minion.transform.position = player.transform.position;
            minion.transform.SetParent(player.transform);

            minion.transform.localPosition = gameLocation;

            minion.transform.localRotation = minionAngle;
            this.transform.localPosition = Vector2.zero;
        }
    }

    public Vector2 hubToGameLocation() 
    {
        //GameObject ship = GameObject.Find("Centerpiece");
        //Vector2 midpoint = ship.GetComponent<RectTransform>().TransformPoint(ship.GetComponent<RectTransform>().rect.center);
        //Vector2 minionPoint = this.GetComponentInParent<RectTransform>().TransformPoint(this.GetComponentInParent<RectTransform>().rect.center);

        ////Debug.Log(midpoint);
        ////Debug.Log(minionPoint);

        //Vector2 offset = minionPoint - midpoint;
        ////Debug.Log(offset);
        ////Vector2 minionPosition = Camera.main.ScreenToWorldPoint(offset);
        //Vector2 minionPosition = Camera.main.ScreenToWorldPoint(new Vector3(offset.x, offset.y, Camera.main.nearClipPlane));

        //minionPosition.x += (8.891599f);
        //minionPosition.y += (3.990846f);

        //return minionPosition;
        //////float scale = (2f * Camera.main.orthographicSize) / canvas.GetComponent<CanvasScaler>().referenceResolution.y;
        ////GameObject ship = GameObject.Find("Centerpiece");
        //////Vector2 offset = ((Vector2)this.transform.position - (Vector2)ship.transform.position)*scale*2f;
        //////return offset;

        GameObject ship = GameObject.Find("Centerpiece");
        Vector2 midpoint = ship.GetComponent<RectTransform>().TransformPoint(ship.GetComponent<RectTransform>().rect.center);
        Vector2 minionPoint = this.GetComponentInParent<RectTransform>().TransformPoint(this.GetComponentInParent<RectTransform>().rect.center);
        
        Debug.Log(midpoint);

        Vector2 offset = minionPoint - midpoint;
        Debug.Log(offset);
        return offset;
    }

}
