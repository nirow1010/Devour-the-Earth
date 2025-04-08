using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    private Transform gridobject;
    public enum DropSpace { shipArea, menuArea }
    public DropSpace droparea;

    private void Awake()
    {
        if (droparea == DropSpace.menuArea) {
            gridobject = this.transform.Find("Viewport").Find("Content");
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropObj = eventData.pointerDrag;
        DragDrop itemScript = dropObj.GetComponent<DragDrop>();
        //Debug.Log("OnDrop");

        if (eventData.pointerDrag != null) {

            switch (droparea)
            {
                case DropSpace.shipArea:
                    if (!itemScript.overlap)
                    {
                        itemScript.onArea = true;
                        itemScript.CanRotate(true);
                        itemScript.home = dropObj.GetComponent<RectTransform>().anchoredPosition;
                        dropObj.GetComponentInChildren<MinionData>().hubLocation = itemScript.home;
                        dropObj.GetComponentInChildren<MinionData>().gameLocation = itemScript.home;
                        dropObj.GetComponentInChildren<MinionData>().active = true;
                        //Debug.Log("Shiparea");
                    }
                    break;

                case DropSpace.menuArea:
                    dropObj.transform.SetParent(gridobject);
                    itemScript.onArea = false;
                    itemScript.CanRotate(false);
                    dropObj.GetComponentInChildren<MinionData>().active = false;
                    //Debug.Log("menuarea");
                    break;
            
            }
        }
    }
}
