using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool working = true;
    public float pushForce = 5f;
    public float maxSpeed = 4f;
    public float yOffsetSpeed = 1f;

    private MeshRenderer forceMesh;
    private Material meshMaterial;
    private void Start()
    {
        forceMesh = GetComponent<MeshRenderer>();
        meshMaterial = forceMesh.material;
    }
    private void Update()
    {
        if (working==true && forceMesh.enabled==false)
        {
            forceMesh.enabled = true;
        }
        else if (working==false && forceMesh.enabled==true)
        {
            forceMesh.enabled = false;
        }
        if (working == true)
        {
            float newYOffset = meshMaterial.mainTextureOffset.y + yOffsetSpeed * Time.deltaTime;
            meshMaterial.mainTextureOffset = new Vector2(meshMaterial.mainTextureOffset.x, newYOffset);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (working == true && other.GetComponent<Rigidbody>().velocity.y <= maxSpeed)
            {
                other.GetComponent<Rigidbody>().AddForce(transform.up * pushForce * 0.1f, ForceMode.VelocityChange);
            }
        }
    }

}
