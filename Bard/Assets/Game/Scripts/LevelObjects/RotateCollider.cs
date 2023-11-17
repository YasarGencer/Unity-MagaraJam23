using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other != null)
            if (other.transform.CompareTag("Player"))
                if (other.transform.position.x >= 0)
                    MainManager.Instance.TowerManager.RotateRight();
                else
                    MainManager.Instance.TowerManager.RotateLeft();
    }
}
