using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionData : MonoBehaviour
{
    public GameObject shipType;
    public Vector2 hubLocation;
    public Vector2 gameLocation;
    public bool active;

    public bool runStart;

    [SerializeField] private GameObject StickerPrefab;
    [SerializeField] private Transform grid;
    [SerializeField] private Canvas canvas;

    private void Update()
    {
        if (runStart && SceneManager.GetActiveScene().name == "Hub")
        {
            runStart = false;
            setup();
        }
    }

    private void setup() 
    {
        grid = GameObject.Find("Canvas").transform.Find("Ship Menu").Find("Viewport").Find("Content");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Hub")
        {
            if (active)
            {
                Debug.Log("Hello");
                GameObject sticker = Instantiate(StickerPrefab);
                sticker.transform.SetParent(canvas.transform, true);
                sticker.GetComponent<RectTransform>().anchorMax = new Vector2(0,1);
                sticker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                transform.SetParent(sticker.transform);
                sticker.GetComponent<RectTransform>().anchoredPosition = GetComponent<MinionData>().hubLocation;
                sticker.GetComponent<RectTransform>().localScale = Vector3.one;
            }
            else
            {
                GameObject sticker = Instantiate(StickerPrefab);
                sticker.transform.SetParent(grid);
                sticker.GetComponent<RectTransform>().localScale = Vector3.one;
                transform.SetParent(sticker.transform);
            }
        }
    }

}
