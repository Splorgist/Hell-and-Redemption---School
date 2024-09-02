using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    Animator _animator;
    string _currentState;

    const string BUTTON_DESELECTION = "button_deselection";
    const string BUTTON_PRESS = "button_press";
    const string BUTTON_SELECTED = "button_selected";

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (newState == _currentState){
            return;
        }

        _animator.Play(newState);
        _currentState = newState;
    }
}
