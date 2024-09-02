using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
     public GameObject targetObject;

    // This will be set in the Inspector
    private string targetName;

    private void Start()
    {
        // Initialize targetName based on the assigned GameObject
        if (targetObject != null)
        {
            targetName = targetObject.name;
        }
        else
        {
            Debug.LogWarning("Target Object is not assigned!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if the current GameObject matches the assigned targetObject
        if (targetObject != null && gameObject == targetObject)
        {
            Debug.Log("Mouse is over: " + targetObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Check if the current GameObject matches the assigned targetObject
        if (targetObject != null && gameObject == targetObject)
        {
            Debug.Log("Mouse exited: " + targetObject.name);
        }
    }
}
