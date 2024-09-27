using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Transitioned : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu1;
    public GameObject menu2;
    public int transition_detect;
    public bool transition = true;

    public GameObject button1;
    public GameObject button2;


    public bool button1_down = false;
    public bool button2_down = false;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;


    void Start()
    {
        anim1.Play("button1_fade_in");
        anim2.Play("button1_fade_in");
        anim3.Play("quit_text_fade_in");
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo1 = anim1.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo stateInfo2 = anim2.GetCurrentAnimatorStateInfo(0);
       
        if (button1_down && Input.GetMouseButtonDown(0)){
            StartCoroutine (YesQuit());
        }else if (button2_down && Input.GetMouseButtonDown(0)){
            StartCoroutine (NoQuit());
        }

        if (stateInfo1.IsName("button1_pressed")){
            transition = true;
        }else if (stateInfo2.IsName("button1_pressed")){
            Debug.Log("Pressed No button");
            transition = true;
        }else{
            transition = false;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name && transition == false){
            Debug.Log("Play selected for button1");
            anim1.Play("button1_select");
            button1_down = true;
        }else if (eventData.pointerEnter.name == button2.name && transition == false){
            Debug.Log("Play selected for button1");
            anim2.Play("button1_select");
            button2_down = true;
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button1.name && transition == false){
            Debug.Log("Play deselect for " + button1.name);
            button1_down = false;
            anim1.Play("button1_deselect");
        }else if (eventData.pointerEnter.name == button2.name && transition == false){
            Debug.Log("Play deselect for " + button1.name);
            button2_down = false;
            anim2.Play("button1_deselect");
        }
    }


    private IEnumerator YesQuit()
    {
        anim1.Play("button1_pressed");
        anim2.Play("button1_fade");
        anim3.Play("quit_text_fade");


        Debug.Log("Menu transition");


        yield return new WaitForSeconds(1);


        Debug.Log("Disable assets");
        menu2.SetActive(false);
    }
    private IEnumerator NoQuit()
    {
        anim1.Play("button1_fade");
        anim2.Play("button1_pressed");
        anim3.Play("quit_text_fade");

        Debug.Log("Menu transition");


        yield return new WaitForSeconds(1);


        Debug.Log("Disable assets");
        menu1.SetActive(true);
        menu2.SetActive(false);

    }
}

