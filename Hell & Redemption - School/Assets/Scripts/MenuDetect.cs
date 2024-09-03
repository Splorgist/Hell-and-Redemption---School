using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu;
    public Animator animator;
    public Animator image;

    private bool isPointerOver = false;
    public bool clickable = true;

    private void Start()
    {
        menu.name = name;
    }

    private void Update()
    {
        if (isPointerOver && Input.GetMouseButtonDown(0) && menu.name == "Q")
        {
            Debug.Log("Exit");
            animator.SetTrigger("deselect");
            Cursor.lockState = CursorLockMode.Locked;
            image.SetTrigger("fade_in");
        }else if (isPointerOver && Input.GetMouseButtonDown(0) && menu.name == "O"){
            Debug.Log("Options");
            animator.SetTrigger("deselect");
            Cursor.lockState = CursorLockMode.Locked;
            image.SetTrigger("fade_in");
        }else if (isPointerOver && Input.GetMouseButtonDown(0) && menu.name == "LG"){
            Debug.Log("Load Game");
            animator.SetTrigger("deselect");
            Cursor.lockState = CursorLockMode.Locked;
            image.SetTrigger("fade_in");
        }else if (isPointerOver && Input.GetMouseButtonDown(0) && menu.name == "Start"){
            Debug.Log("Start Game");
            animator.SetTrigger("deselect");
            Cursor.lockState = CursorLockMode.Locked;
            image.SetTrigger("fade_in");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered " + menu.name);
        animator.SetTrigger("select");

        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited " + menu.name);
        animator.SetTrigger("deselect");

        isPointerOver = false;
    }
}
