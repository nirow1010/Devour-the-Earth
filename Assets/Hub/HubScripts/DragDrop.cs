using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,
    IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private RotateObj roateScript;
    private bool mouseOver;
    public bool overlap;

    [SerializeField] private GameObject inventory;
    public Vector2 home;
    public bool onArea;
    public int minionNodeNum;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        roateScript = GetComponent<RotateObj>();
        inventory = GameObject.Find("Ship Menu");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropArea>() == null || overlap)
        {
            MinionData minionDataScript = GetComponentInChildren<MinionData>();
            if (onArea)
            {

                //Debug.Log("AAA");
                rectTransform.anchoredPosition = home;
                minionDataScript.hubLocation = home;
                minionDataScript.gameLocation = GetComponentInChildren<MinionData>().hubToGameLocation();
                minionDataScript.active = true;
            }
            else
            {
                //Debug.Log("AAAA");
                CanRotate(false);
                transform.localEulerAngles = new Vector3(0,0,0);
                Transform gridObject = inventory.transform.Find("Viewport").Find("Content");
                this.transform.SetParent(gridObject);
                minionDataScript.active = false;
            }
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void CanRotate(bool rotateable) {
        if (rotateable)
            roateScript.enabled = true;
        else
            roateScript.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("StickerOverlap") || other.CompareTag("Sticker"))
        {
            overlap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Exit");
        if (other.CompareTag("StickerOverlap") || other.CompareTag("Sticker"))
        {
            overlap = false;
        }
    }
}
