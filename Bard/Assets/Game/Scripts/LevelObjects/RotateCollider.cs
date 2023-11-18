using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RotateCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other != null)
            if (other.transform.CompareTag("Player"))
            {
                Rotator(other.transform.position.x >= 0, other.transform);
            }
                
    }
    void Rotator(bool right, Transform player) {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        
        player.GetComponent<PlayerMovementScript>().IsRotating = true;
        player.DOMove(new Vector3(transform.position.x, player.position.y, transform.position.z), .15f).OnComplete(() => {

            if (right) {
                MainManager.Instance.TowerManager.RotateRight();
            } else {
                MainManager.Instance.TowerManager.RotateLeft();
            }
        });
    }
}
