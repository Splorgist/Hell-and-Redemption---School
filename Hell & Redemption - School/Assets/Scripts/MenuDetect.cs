using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;


    public bool button1_down = false;
    public bool button2_down = false;
    public bool button3_down = false;
    public bool button4_down = false;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name){
            Debug.Log("Play selected for button1");
            anim1.Play("button1_select");
        }else if (eventData.pointerEnter.name == button2.name){
            Debug.Log("Play selected for button2");
            anim2.Play("button2_select");
        }else if (eventData.pointerEnter.name == button3.name){
            Debug.Log("Play selected for button3");
            anim3.Play("button3_select");
        }else if (eventData.pointerEnter.name == button4.name){
            Debug.Log("Play selected for button4");
            anim4.Play("button4_select");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name){
            Debug.Log("Play deselect for " + button1.name);
            anim1.Play("button1_deselect");
        }else if (eventData.pointerEnter.name == button2.name){
            Debug.Log("Play deselect for " + button2.name);
            anim2.Play("button2_deselect");
        }else if (eventData.pointerEnter.name == button3.name){
            Debug.Log("Play deselect for " + button3.name);
            anim3.Play("button3_deselect");
        }else if (eventData.pointerEnter.name == button4.name){
            Debug.Log("Play deselect for button4" + button4.name);
            anim4.Play("button4_deselect");
        }
    }
}
