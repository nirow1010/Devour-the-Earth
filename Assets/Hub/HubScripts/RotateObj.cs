using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateObj : MonoBehaviour, IScrollHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private bool mouseOver;
    private DragDrop dragObjScript;
    private MinionData minionDataScript;

    public float rotationSpeed;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        dragObjScript = GetComponent<DragDrop>();
    }

    public void OnScroll(PointerEventData eventData)
    {
        if(minionDataScript == null)
            minionDataScript = GetComponentInChildren<MinionData>();

        if (dragObjScript.onArea && mouseOver)
        {
            float scrollAmount = eventData.scrollDelta.y;

            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.z += scrollAmount * rotationSpeed;

            transform.localEulerAngles = currentRotation;
            minionDataScript.minionAngle = rectTransform.rotation;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
