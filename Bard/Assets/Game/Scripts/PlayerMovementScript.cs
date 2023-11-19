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

    //private CharacterController characterController;
    public Rigidbody rb;
    [SerializeField] Animator animator;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = 9.8f;
    public float jumpHeight = 3f;
    public float rayDistance = 0.55f;
    public float vectorOffset = 0.25f;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool IsRotating;
    public ParticleSystem diamondParticle;
    public float elevate = 0.01f;
    private GameObject rayTransformGameObject;
    private float rotationSpeed = 90f;
    private Vector3 defaultScale;
    [SerializeField]private bool isGrounded;
    private bool canJump = true;
    public bool isClimbing=false;
    public bool isMenu = false;

    [SerializeField] private AudioSource walkSound;
    //[SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource plateSound;

    private void Awake() {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        //characterController = GetComponent<CharacterController>();
    }
    void Start() {
        defaultScale = transform.localScale;
        Physics.IgnoreLayerCollision(3, 6);



    }

    // Update is called once per frame
    void Update() {
        CheckGround();
        Move();
        
    }
    private void Move() {
        if (IsRotating == false) {
            float directionX = Math.Abs(inputManager.move.x) > 0.6 ? 1 * (inputManager.move.x / Math.Abs(inputManager.move.x)) : 0;
            if (inputManager.move.x >= .5) {
                transform.localScale = new Vector3(defaultScale.x, transform.localScale.y, transform.localScale.z);
            } else if (inputManager.move.x <= -.5) {
                transform.localScale = new Vector3(-defaultScale.x, transform.localScale.y, transform.localScale.z);
            }
            Vector3 moveDirection = new Vector3(directionX, 0f, 0f);
            moveDirection = transform.TransformDirection(moveDirection); // Y�n� objenin y�n�ne �evir

            float speed = inputManager.run ? runSpeed : moveSpeed;

            if (inputManager.run) {
                inputManager.runTimer += Time.deltaTime;
                if (inputManager.runTimer >= inputManager.maxRunTime) {
                    inputManager.run = false;
                }
            }


            if(moveDirection.x != 0 && isGrounded) {
                animator.SetFloat("speed", inputManager.run ? 1 : .5f);
                if (!walkSound.isPlaying) {
                    StartCoroutine(FadeIn(walkSound, .2f));
                    walkSound.Play();   
                }
                //if (inputManager.run) {
                //    if (!runSound.isPlaying) {
                //        StartCoroutine(FadeIn(runSound, .2f));
                //        runSound.Play();
                //    }
                //    if (walkSound.isPlaying) {
                //        StartCoroutine(FadeOut(walkSound, .2f));
                //    }
                //}
                //else {
                //    if (!walkSound.isPlaying) {
                //        StartCoroutine(FadeIn(walkSound, .2f));
                //        walkSound.Play();
                //    }
                //    if (runSound.isPlaying) {
                //        StartCoroutine(FadeOut(runSound, .2f));
                //    }
                //}
            } else {
                animator.SetFloat("speed", 0);
                if (walkSound.isPlaying) {
                    StartCoroutine(FadeOut(walkSound, .2f));
                }
                //if (runSound.isPlaying) {
                //    StartCoroutine(FadeOut(runSound, .2f));
                //}
            }

            //characterController.Move(moveDirection * speed * Time.deltaTime);
            //rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            //rb.DOMove(rb.position + moveDirection * speed * Time.deltaTime,0);
            //var yAxis=isClimbing && canJump ? inputManager.move.y : rb.velocity.y;

            if (isClimbing && canJump)
                rb.velocity = new Vector3(rb.velocity.x, inputManager.move.y, rb.velocity.z);

            //rb.velocity = new Vector3(directionX * speed, yAxis, rb.velocity.z);  //old system
            if (MathF.Abs(rb.velocity.x)<=speed || (rb.velocity.x/MathF.Abs(rb.velocity.x) != (directionX/ MathF.Abs(directionX))))
            {
                rb.AddRelativeForce(new Vector3(directionX * speed * 3, 0, 0));
            }
                
                //rb.velocity = new Vector3(MathF.Abs(rb.velocity.x) > MathF.Abs(runSpeed)? (rb.velocity.x/MathF.Abs(rb.velocity.x) * moveSpeed) : rb.velocity.x, yAxis, rb.velocity.z);
            Jump();
            
        } else {
            animator.SetFloat("speed", 0);
        }
    }
    private void Jump() {
        if (inputManager.jump && isGrounded==true && canJump) 
        {
            isGrounded = false;
            canJump = false;
            StartCoroutine(LocalDelay());
            IEnumerator LocalDelay()
            {
                yield return new WaitForSeconds(.5f);
                canJump = true;
            }
            rb.AddForce(0,Mathf.Sqrt(2f * jumpHeight),0,ForceMode.Impulse);
            animator.SetTrigger("jump");
            jumpSound.Play();
        }
        inputManager.jump = false;
    }



    public void RotateCaller(float angle, Vector3 axis, float duration, bool right) {
        StartCoroutine(RotatePlayer(angle, axis, duration, right));
    }

    public IEnumerator RotatePlayer(float angle, Vector3 axis, float duration, bool right) {
        yield return new WaitForEndOfFrame();
        rb.useGravity = false;
        var rotation = new Vector3(transform.rotation.x, transform.rotation.y + angle, transform.rotation.z);
        transform.DOLocalRotate(rotation, duration, RotateMode.LocalAxisAdd).OnComplete(() => {

            Vector3 currentRotation = transform.eulerAngles;
            float newYRotation = Mathf.Round(currentRotation.y / 90) * 90;
            transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

            Vector3 moveDirection = Vector3.zero;
            //moveDirection = transform.TransformDirection(transform.right); // Y�n� objenin y�n�ne �evir
            /*float tempRotate = transform.localRotation.eulerAngles.y;
            Debug.Log("tempRotate: " + tempRotate);
            if (tempRotate == 0 || tempRotate == 360)
            {
                Debug.Log("Rotate.y:0||360");

                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
            else if (tempRotate == -90 || tempRotate == 270)
            {
                Debug.Log("Rotate.y:-90||270");
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            }
            else if (tempRotate == -180 || tempRotate == 180)
            {
                Debug.Log("Rotate.y:-180||180");
                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
            else if (tempRotate == 90 || tempRotate == -270)
            {
                Debug.Log("Rotate.y:90||-270");
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            }*/
            var value = (right == true ? 1 : -1);
            //var value = transform.localScale.x > 0 ? 1 : -1;
            if (isMenu==false)
            {
                transform.DOMove(transform.position + transform.right * value, .25f).OnComplete(() =>
                {
                    IsRotating = false;
                    rb.useGravity = true;
                });
            }
            

        });

    }
    private void OnDrawGizmos()
    {
        // Ray ba�lang�� noktalar�n� belirle
        Vector3 rayOriginRight =transform.position + Vector3.right* vectorOffset;
        Vector3 rayOriginLeft = transform.position - Vector3.right*vectorOffset;

        // Gizmos ile ray'leri 
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rayOriginRight, Vector3.down * rayDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(rayOriginLeft, Vector3.down * rayDistance);
    }

    private void CheckGround()
    {
        // Rayleri a�a�� do�ru g�nder
        RaycastHit hitRight, hitLeft;

        // Sa� ray
        if (Physics.Raycast(transform.position + Vector3.right * vectorOffset, Vector3.down, out hitRight, rayDistance))
        {
            //Debug.Log("Right Ray hit something!");
        }

        // Sol ray
        if (Physics.Raycast(transform.position - Vector3.right * vectorOffset, Vector3.down, out hitLeft, rayDistance))
        {
            //Debug.Log("Left Ray hit something!");
        }

        // Ray'lerden biri bir �eye temas etti mi?
        if (isClimbing==false)
        {
            isGrounded = hitRight.collider != null || hitLeft.collider != null;
            if(!isGrounded && canJump)
                animator.SetTrigger("instantFall");
            animator.SetBool("grounded", isGrounded);
        }
/*
        if (hitLeft.collider!=null && hitLeft.collider.CompareTag("MovingPlatform"))
        {
            //characterController.Move(new Vector3(0,transform.position.y - hitLeft.collider.transform.position.y + elevate, 0));
        }
        else if (hitRight.collider != null && hitRight.collider.CompareTag("MovingPlatform"))
        {
            //characterController.Move(new Vector3(0, transform.position.y - hitRight.collider.transform.position.y + elevate, 0));
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            isGrounded = true;
            animator.SetBool("climb",true);
            if (inputManager.move.y == 0)
                animator.speed = 0;
            else
                animator.speed = 1;
            rb.useGravity = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plate")) {
            plateSound.Play();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            animator.SetBool("climb", false);
            animator.speed = 1;
            rb.useGravity = true;
        }
    }

    // Fade in efekti için
    IEnumerator FadeIn(AudioSource audioSource, float duration) {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 1f) {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }
    }

    // Fade out efekti için
    IEnumerator FadeOut(AudioSource audioSource, float duration) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f) {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }


}