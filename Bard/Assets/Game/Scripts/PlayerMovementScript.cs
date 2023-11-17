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
            RotateObject90Degrees();
        }

        if (Input.GetKeyDown(KeyCode.Y) && !isRotating)
        {
            RotateObjectNegative90Degrees();
        }
    }
    private void Move()
    {
        float directionX = Math.Abs(inputManager.move.x) > 0.6 ? 1 * (inputManager.move.x / Math.Abs(inputManager.move.x)) : 0;

        Vector3 moveDirection = new Vector3(directionX, 0f, 0f);
        moveDirection = transform.TransformDirection(moveDirection); // Yönü objenin yönüne çevir

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        ApplyGravity();
        Jump();
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
    void RotateObject90Degrees()
    {
        StartCoroutine(Rotate(90f));
    }

    void RotateObjectNegative90Degrees()
    {
        StartCoroutine(Rotate(-90f));
    }

    IEnumerator Rotate(float angle)
    {
        // Dönme iþlemi baþladýðýnda isRotating'i true yap
        isRotating = true;

        float elapsedTime = 0f;
        float duration = 1f / rotationSpeed; // Sürekli dönmeyi 1 saniyede tamamlamak için
        Vector3 currentRotation = transform.eulerAngles;
        float newYRotation = Mathf.Round((currentRotation.y / 90) * 90)+angle;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isRotating = false;
    }
}
