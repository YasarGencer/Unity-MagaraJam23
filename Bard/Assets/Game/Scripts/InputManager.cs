using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    public Vector2 move;
    public bool jump;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    private void OnJump()
    {
        jump = true;
    }

}
