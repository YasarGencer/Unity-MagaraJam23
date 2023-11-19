using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_Cube : MonoBehaviour
{

    [SerializeField] bool isCatch = false;
    [SerializeField] string tag = "Player";
    private void OnCollisionEnter(Collision collision)
    {
        if (isCatch == false)
            return;
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.transform.SetParent(this.transform, true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (isCatch == false)
            return;
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.transform.SetParent(this.transform.parent.parent, true);
        }
    }
}
