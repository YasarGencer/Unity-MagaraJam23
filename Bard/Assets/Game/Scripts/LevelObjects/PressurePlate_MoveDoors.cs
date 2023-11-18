using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_MoveDoors : MonoBehaviour
{
    [SerializeField] Transform door, endPosition;
    [SerializeField] float time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            OpenDoor();
    }

    private void OpenDoor()
    {
        door.DOLocalMove(endPosition.localPosition, time);
    }
}
