using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objTrue;
    public GameObject objTrue2;
    public GameObject objFalse;
    public GameObject objFalse2;
    void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TheCube"))
        {
            OpenCloser();
        }

    }
    private void OpenCloser()
    {
        if (objTrue != null)
        {
            objTrue.SetActive(true);
        }
        if (objTrue2 != null)
        {
            objTrue2.SetActive(true);
        }
        if (objFalse != null)
        {
            objFalse.SetActive(false);
        }
        if (objFalse2 != null)
        {
            objFalse2.SetActive(false);
        }
    }
}
