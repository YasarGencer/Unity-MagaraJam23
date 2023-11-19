using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    public Animator animator;


    void Start()
    {
        animator.SetTrigger("Once");
    }
}
