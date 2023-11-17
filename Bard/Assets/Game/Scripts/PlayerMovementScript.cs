using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour {
    private InputManager inputManager;

    private CharacterController characterController;
    [SerializeField] Animator animator;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = 9.8f;
    public float jumpHeight = 3f;
    public float rayDistance = 0.55f;
    public float vectorOffset = 0.25f;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool IsRotating;
    private float rotationSpeed = 90f;
    private Vector3 defaultScale;
    private bool isGrounded;
    private void Awake() {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }
    void Start() {
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        CheckGround();
        Move();
        
    }
    private void Move() {
        if (IsRotating == false) {
            float directionX = Math.Abs(inputManager.move.x) > 0.6 ? 1 * (inputManager.move.x / Math.Abs(inputManager.move.x)) : 0;
            if (inputManager.move.x == 1) {
                transform.localScale = new Vector3(defaultScale.x, transform.localScale.y, transform.localScale.z);
            } else if (inputManager.move.x == -1) {
                transform.localScale = new Vector3(-defaultScale.x, transform.localScale.y, transform.localScale.z);
            }
            Vector3 moveDirection = new Vector3(directionX, 0f, 0f);
            moveDirection = transform.TransformDirection(moveDirection); // Yönü objenin yönüne çevir

            float speed = inputManager.run ? runSpeed : moveSpeed;

            if (inputManager.run) {
                inputManager.runTimer += Time.deltaTime;
                if (inputManager.runTimer >= inputManager.maxRunTime) {
                    inputManager.run = false;
                }
            }


            if(moveDirection.x != 0)
                animator.SetFloat("speed", inputManager.run ? 1 : .5f);
            else
                animator.SetFloat("speed", 0);

            characterController.Move(moveDirection * speed * Time.deltaTime);

            Jump();
            ApplyGravity();
            
        } else {
            animator.SetFloat("speed", 0);
        }
    }

    private void ApplyGravity() {
        if (isGrounded == false) 
        {
            velocity.y -= gravity * Time.deltaTime;
            if (velocity.y<=-gravity)
            {
                velocity.y = -gravity;
            }
        } 
        characterController.Move(velocity * Time.deltaTime);
    }
    private void Jump() {
        if (inputManager.jump && isGrounded==true) 
        {
            isGrounded = false;
            velocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
        }
        inputManager.jump = false;
    }



    public void RotateCaller(float angle, Vector3 axis, float duration) {
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
    public IEnumerator RotatePlayer(float angle, Vector3 axis, float duration) {
        yield return new WaitForEndOfFrame();
        var rotation = new Vector3(transform.rotation.x, transform.rotation.y + angle, transform.rotation.z);
        transform.DOLocalRotate(rotation, duration, RotateMode.LocalAxisAdd).OnComplete(() => {

            Vector3 currentRotation = transform.eulerAngles;
            float newYRotation = Mathf.Round(currentRotation.y / 90) * 90;
            transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

            Vector3 moveDirection = Vector3.zero;
            //moveDirection = transform.TransformDirection(transform.right); // Yönü objenin yönüne çevir

            int value = transform.localScale.x > 0 ? 1 : -1;
            transform.DOMove(transform.position + transform.right * value, .25f).OnComplete(() => IsRotating = false);

        });

    }
    private void OnDrawGizmos()
    {
        // Ray baþlangýç noktalarýný belirle
        Vector3 rayOriginRight = transform.position + Vector3.right* vectorOffset;
        Vector3 rayOriginLeft = transform.position - Vector3.right*vectorOffset;

        // Gizmos ile ray'leri 
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rayOriginRight, Vector3.down * rayDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(rayOriginLeft, Vector3.down * rayDistance);
    }

    private void CheckGround()
    {
        // Rayleri aþaðý doðru gönder
        RaycastHit hitRight, hitLeft;

        // Sað ray
        if (Physics.Raycast(transform.position + Vector3.right * vectorOffset, Vector3.down, out hitRight, rayDistance))
        {
            Debug.Log("Right Ray hit something!");
        }

        // Sol ray
        if (Physics.Raycast(transform.position - Vector3.right * vectorOffset, Vector3.down, out hitLeft, rayDistance))
        {
            Debug.Log("Left Ray hit something!");
        }

        // Ray'lerden biri bir þeye temas etti mi?
        isGrounded = hitRight.collider != null || hitLeft.collider != null;
    }
}