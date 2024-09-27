using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Transitioned3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu1;
    public GameObject menu2;
    public int transition_detect;
    public bool transition = true;

    public GameObject button2;


    public bool button2_down = false;

    public Animator anim1;
    public Animator anim2;


    void Start()
    {
        anim2.Play("button1_fade_in");
        anim1.Play("mult_fade_in");
    }

    private void OnEnable()
    {
        anim2.Play("button1_fade_in");
        anim1.Play("mult_text_fade_in");
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo2 = anim2.GetCurrentAnimatorStateInfo(0);
       
        if (button2_down && Input.GetMouseButtonDown(0)){
            StartCoroutine (NoQuit());
        }

        if (stateInfo2.IsName("button1_pressed")){
            Debug.Log("Pressed No button");
            transition = true;
        }else{
            transition = false;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button2.name && transition == false){
            Debug.Log("Play selected for button1");
            anim2.Play("button1_select");
            button2_down = true;
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited button: " + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == button2.name && transition == false){
            Debug.Log("Play deselect for " + button2.name);
            button2_down = false;
            anim2.Play("button1_deselect");
        }
    }

    private IEnumerator NoQuit()
    {
        anim2.Play("button1_pressed");
        anim1.Play("mult_fade_out");

        Debug.Log("Menu transition");


        yield return new WaitForSeconds(1);


        Debug.Log("Disable assets");
        menu1.SetActive(true);
        menu2.SetActive(false);

    }
}

