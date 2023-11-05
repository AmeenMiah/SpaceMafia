using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationVariables : MonoBehaviour
{
    public Animator anim;
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isjumping", obj.GetComponent<ThirdPersonMovement>().isJumping);
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        anim.SetBool("sprint", obj.gameObject.GetComponent<ThirdPersonMovement>().isSprinting);
    }
}
