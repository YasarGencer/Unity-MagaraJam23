using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    InputActions inputActions;
    public Vector2 move;
    public bool jump;
    public bool run;
    public float runTimer = 0f;
    public float maxRunTime = 1f;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        //inputActions = GetComponent<InputActions>();
        inputActions = new();
        inputActions.Enable();
        inputActions.Player.Run.started += OnRunStarted;
        inputActions.Player.Run.canceled += OnRunCanceled;
    }
    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    private void OnJump()
    {
        jump = true;
    }private void OnRun()
    {

    }
    private void OnRunStarted(InputAction.CallbackContext context)
    {
        if (!run)
        {
            run = true;
            runTimer = 0f;
        }
    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        run = false;
    }
   // private void OnRunCanceled() {  run = false; }

   

}
