using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;


    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpWasReleased;
    public static bool RunIsHeld;
    public static bool Attack;
    public static bool Exit;


    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _attackAction;
    private InputAction _exitGame;


    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        _attackAction = PlayerInput.actions["Attack"];
        _exitGame = PlayerInput.actions["Exit"];
    }


    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        JumpWasPressed = _jumpAction.WasPerformedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();
        Attack = _attackAction.WasPerformedThisFrame();
        Exit = _exitGame.WasPerformedThisFrame();




        RunIsHeld = _runAction.IsPressed();
    }
}
