using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> targetObjects = new List<GameObject>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var obj in targetObjects)
        {
            if (eventData.pointerEnter == obj)
            {
                Debug.Log("Mouse is over: " + obj.name);
                return; // Exit the loop once the object is found
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var obj in targetObjects)
        {
            if (eventData.pointerEnter == obj)
            {
                Debug.Log("Mouse exited: " + obj.name);
                return; // Exit the loop once the object is found
            }
        }
    }
}
