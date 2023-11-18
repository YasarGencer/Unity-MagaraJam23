using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTurner : MonoBehaviour
{
    public WheelRotate wheelRotate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wheelRotate.enabled = true;
        }
            
    }
}
