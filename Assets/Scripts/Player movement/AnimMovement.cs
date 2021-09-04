using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMovement : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ismidair = animator.GetBool("isMidair");       
        bool isjumping = animator.GetBool("isJumping");
        bool isrunning = animator.GetBool("isRunning");
        bool iswalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown("space");

        //Walking forward
        if (!iswalking && forwardPressed){
            animator.SetBool("isWalking", true);
        }
        if (iswalking && !forwardPressed){
            animator.SetBool("isWalking", false);
        }

        //Running forward
        if (isrunning && (!forwardPressed || !runPressed)){
            animator.SetBool("isRunning", false);
        }
        if (!isrunning && (forwardPressed && runPressed)){
            animator.SetBool("isRunning", true);
        }

        //Jumping Idle
        if (!isjumping && jumpPressed){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);
        }
        if (isjumping && !jumpPressed){
            animator.SetBool("isJumping", false);
        }


        //Jumping while walking
        if (iswalking && !isjumping && (forwardPressed && jumpPressed)){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);

        }
        if (!iswalking && !isjumping && (!forwardPressed && !jumpPressed)){
            animator.SetBool("isJumping", false);
        }

        //Jumping while running
        if (isrunning && !isjumping && (forwardPressed && runPressed && jumpPressed)){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);

        }
        if (!iswalking && !isjumping && (!forwardPressed && !runPressed && !jumpPressed)){
            animator.SetBool("isJumping", false);
        }
    }
}
