﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerAnimations : MonoBehaviour
{
    private Animator            anim;
    private PlayerController    controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        // Only for testing
        PlayerWalking(); 
    }

    public void PlayerWalking()
    {
        if (anim != null)
            // Walking animation
            //anim.SetBool("IsWalking", controller.isMoving);

        Debug.Log("Walking animation");
    }

    // For form changes (to be implemented later on once other scripts are made)
    void SwitchForAnim()
    {

    }
}
