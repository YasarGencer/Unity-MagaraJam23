using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_OpenCloser : MonoBehaviour
{
    [SerializeField] GameObject obj;
    public GameObject taheravalli;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenCloser();
            if (taheravalli!=null)
            {
                taheravalli.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
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
