using DG.Tweening;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset;
    [SerializeField] private Transform star;

    Camera cam;
    private void Start() {
        cam = GetComponent<Camera>();
    }
    private void Update() {
        //transform.position = new(transform.position.x, player.position.y + offset, transform.position.z);
        star.transform.LookAt(transform.position);
          
        var mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        var targetPos = (player.position + mousePos) / 2f;
        targetPos.x = transform.position.x;
        float invertedY = Mathf.Lerp(player.position.y - 2f, player.position.y + 2f, 0.5f);
        float smoothY = Mathf.Lerp(transform.position.y, invertedY + (invertedY - targetPos.y), Time.deltaTime * 5f); // Zaman gecikmesi ekleyerek smooth hareket
        targetPos.y = smoothY;
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }
}
