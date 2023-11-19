using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuAnimController : StateMachineBehaviour
{
    
    public Animator animator;


    void Start()
    {
        animator.SetTrigger("Once");
    }
}
