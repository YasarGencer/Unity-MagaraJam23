using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private InputManager inputManager;

    private CharacterController characterController;
    public float moveSpeed = 5f;
    public float gravity = 9.8f;
    public float jumpHeight = 3f;
    private Vector3 velocity;
    private bool isRotating;
    private float rotationSpeed = 90f;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.T) && !isRotating)
        {
            RotateCaller(90,Vector3.up,1);
        }

        if (Input.GetKeyDown(KeyCode.Y) && !isRotating)
        {
            RotateCaller(90, Vector3.up, 1);
        }
    }
    private void Move()
    {
        if (characterController.enabled == true)
        {

        
        float directionX = Math.Abs(inputManager.move.x) > 0.6 ? 1 * (inputManager.move.x / Math.Abs(inputManager.move.x)) : 0;

        Vector3 moveDirection = new Vector3(directionX, 0f, 0f);
        moveDirection = transform.TransformDirection(moveDirection); // Yönü objenin yönüne çevir

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        ApplyGravity();
        Jump();
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded == false)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {
        if (inputManager.jump && characterController.velocity.y == 0f)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
        }
        inputManager.jump = false;
    }

    public void RotateCaller(float angle, Vector3 axis, float duration)
    {
        StartCoroutine(RotatePlayer(angle, axis, duration));
    }
    /*public void RotatePlayer(float angle, Vector3 axis, float duration)
    {
        characterController.enabled = false;
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(axis * angle) * initialRotation;
        transform.DOLocalRotateQuaternion(targetRotation, 2);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        isRotating = false;
       // characterController.enabled = true;
    }*/
    public IEnumerator RotatePlayer(float angle, Vector3 axis, float duration)
    {
        characterController.enabled = false;
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(axis * angle) * initialRotation;
        transform.DOLocalRotateQuaternion(targetRotation, duration);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isRotating = false;
        characterController.enabled = true;
    }
}
