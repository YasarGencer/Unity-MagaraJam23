using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_OpenCloser : MonoBehaviour
{
    [SerializeField] GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenCloser();
        }
            
    }

    private void OpenCloser()
    {
        if (obj.activeInHierarchy==true)
        {
            obj.SetActive(false);
        }
        else if (obj.activeInHierarchy == false)
        {
            obj.SetActive(true);
        }

    }
}
