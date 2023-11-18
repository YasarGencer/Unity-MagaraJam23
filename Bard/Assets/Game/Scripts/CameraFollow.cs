using DG.Tweening;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset;
    [SerializeField] private Transform star;
    private void Update() {
        transform.position = new(transform.position.x, player.position.y + offset, transform.position.z);
        star.transform.LookAt(transform.position);
    }
}
