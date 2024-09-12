using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] buttons;
    public GameObject[] menus;
    public Animator[] animators;
    private bool[] buttonStates;
    public bool transition = false;
    public int transition_type = 0;


    private void Awake()
    {
        buttonStates = new bool[buttons.Length];
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < buttonStates.Length; i++)
            {
                if (buttonStates[i])
                {
                    StartCoroutine(HandleButtonAction(i));
                    break;
                }
            }
        }


        CheckAnimatorStates();
    }


    private void CheckAnimatorStates()
    {
        transition = false;
        foreach (var animator in animators)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("button1_pressed"))
            {
                transition = true;
                Debug.Log($"Pressed {animator.gameObject.name}");
                break;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        HandlePointerEvent(eventData.pointerEnter, true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        HandlePointerEvent(eventData.pointerEnter, false);
    }


    private void HandlePointerEvent(GameObject pointerObject, bool isEntering)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (pointerObject == buttons[i] && !transition)
            {
                string action = isEntering ? "select" : "deselect";
                animators[i].Play($"button1_{action}");
                buttonStates[i] = isEntering;
                Debug.Log($"{(isEntering ? "Play selected for" : "Play deselect for")} {buttons[i].name}");
                break;
            }
        }
    }


    private IEnumerator HandleButtonAction(int index)
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].Play(i == index ? "button1_pressed" : "button1_fade");
        }


        Debug.Log("Menu transition");


        yield return new WaitForSeconds(1);


        Debug.Log("Disable assets");
        transition_type = index + 1;
        Debug.Log(transition_type);


        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }


        if (index >= 0 && index < menus.Length)
        {
            menus[index].SetActive(true);
        }


    }
}


