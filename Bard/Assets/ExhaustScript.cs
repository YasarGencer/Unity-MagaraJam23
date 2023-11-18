using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExhaustScript : MonoBehaviour
{
    public float pushForce = 8f;
    public float pushDelay = 3f;
    public float startDelay = 0f;
    private bool inCollider;
    private Rigidbody rb;
    private ParticleSystem particle;
    void Start()
    {
        particle=transform.parent.GetComponentInChildren<ParticleSystem>();
        // Coroutine'u baþlat
        StartCoroutine(YazCoroutine());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inCollider = true;
            rb=other.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inCollider = false;
            rb = null;
        }
    }


    IEnumerator YazCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            
            
            yield return new WaitForSeconds(pushDelay);
            Debug.Log("Salam");
            Debug.Log("incollider: " + inCollider);
            particle.Play();
            if (inCollider == true)
            {
                
                rb.AddForce(transform.forward*pushForce, ForceMode.Impulse);
            }
        }
    }
}
