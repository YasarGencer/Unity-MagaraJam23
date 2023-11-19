using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset;
    [SerializeField] private Transform star;
    private void Update()
    {
        transform.position = new(player.position.x+offset, player.position.y, transform.position.z);
        star.transform.LookAt(transform.position);
    }
}
