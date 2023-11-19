using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject objOpen;
    [SerializeField] GameObject objClose;
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
        objOpen.SetActive(true);
        objClose.SetActive(false);
    }
}
