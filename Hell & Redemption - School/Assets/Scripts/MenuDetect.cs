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


    private void Update()
    {

        AnimatorStateInfo stateInfo = anim1.GetCurrentAnimatorStateInfo(0);
        
        if (button1_down && Input.GetMouseButtonDown(0)){
            StartGame();
        }else if (button2_down && Input.GetMouseButtonDown(0)){
            LoadGame();
        }else if (button3_down && Input.GetMouseButtonDown(0)){
            Options();
        }else if (button4_down && Input.GetMouseButtonDown(0)){
            QuitGame();
        }

        // if (stateInfo.IsName("New State")){
        //     Debug.Log("No animation playing");
        // }else{
        //     Debug.Log("Animation is playing");
        // }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name){
            Debug.Log("Play selected for button1");
            anim1.Play("button1_select");
            button1_down = true;
        }else if (eventData.pointerEnter.name == button2.name){
            Debug.Log("Play selected for button1");
            anim2.Play("button1_select");
            button2_down = true;
        }else if (eventData.pointerEnter.name == button3.name){
            Debug.Log("Play selected for button1");
            anim3.Play("button1_select");
            button3_down = true;
        }else if (eventData.pointerEnter.name == button4.name){
            Debug.Log("Play selected for button1");
            anim4.Play("button1_select");
            button4_down = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name){
            Debug.Log("Play deselect for " + button1.name);
            button1_down = false;
            anim1.Play("button1_deselect");
        }else if (eventData.pointerEnter.name == button2.name){
            Debug.Log("Play deselect for " + button1.name);
            button2_down = false;
            anim2.Play("button1_deselect");
        }else if (eventData.pointerEnter.name == button3.name){
            Debug.Log("Play deselect for " + button1.name);
            button3_down = false;
            anim3.Play("button1_deselect");
        }else if (eventData.pointerEnter.name == button4.name){
            Debug.Log("Play deselect for " + button1.name);
            button4_down = false;
            anim4.Play("button1_deselect");
        }
    }

    void StartGame()
    {
        anim1.Play("button1_pressed");
        anim2.Play("button1_fade");
        anim3.Play("button1_fade");
        anim4.Play("button1_fade");

        Debug.Log("Menu transition");
    }

    void LoadGame()
    {
        anim1.Play("button1_fade");
        anim2.Play("button1_pressed");
        anim3.Play("button1_fade");
        anim4.Play("button1_fade");

        Debug.Log("Menu transition");
    }

    void Options()
    {
        anim1.Play("button1_fade");
        anim2.Play("button1_fade");
        anim3.Play("button1_pressed");
        anim4.Play("button1_fade");

        Debug.Log("Menu transition");
    }

    void QuitGame()
    {
        anim1.Play("button1_fade");
        anim2.Play("button1_fade");
        anim3.Play("button1_fade");
        anim4.Play("button1_pressed");

        Debug.Log("Menu transition");
    }
}
