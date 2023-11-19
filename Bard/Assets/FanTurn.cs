using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTurn : MonoBehaviour
{
    private FanScript fanScript;
    public float fanSpeed= 50f;
    void Start()
    {
        fanScript=transform.parent.GetComponentInChildren<FanScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fanScript!=null && fanScript.gameObject.activeInHierarchy)
        {
            transform.transform.GetChild(1).Rotate(Vector3.forward, fanSpeed*10 * Time.deltaTime);
        }
    }
}
