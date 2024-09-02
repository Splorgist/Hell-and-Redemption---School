using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu;
    public Animator animator;

    private void Start()
    {
        menu.name = name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered " + menu.name);
        animator.SetTrigger("select");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited " + menu.name);
        animator.SetTrigger("deselect");
    }
}
