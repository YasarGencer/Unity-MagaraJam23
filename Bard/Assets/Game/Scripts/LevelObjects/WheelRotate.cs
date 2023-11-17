using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Otomatik olarak objeyi döndür
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
