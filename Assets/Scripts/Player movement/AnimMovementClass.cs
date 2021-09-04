using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMovementClass : MonoBehaviour
{
    public static bool ismidair = animator.GetBool("isMidair");       
    public static bool isjumping = animator.GetBool("isJumping");
    public static bool isrunning = animator.GetBool("isRunning");
    public static bool iswalking = animator.GetBool("isWalking");
    public static bool forwardPressed = Input.GetKey("w");
    public static bool runPressed = Input.GetKey("left shift");
    public static bool jumpPressed = Input.GetKeyDown("space");
    public static Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static void walkingAndjumping(){
        if (!iswalking && forwardPressed){
            animator.SetBool("isWalking", true);
        }
        if (iswalking && !forwardPressed){
            animator.SetBool("isWalking", false);
        }
        if (iswalking && !isjumping && (forwardPressed && jumpPressed)){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);
        }
        if (!iswalking && !isjumping && (!forwardPressed && !jumpPressed)){
            animator.SetBool("isJumping", false);
        }
        animator.SetBool("isWalking", true);
    }

    public static void runningAndjumping (){
        if (isrunning && (!forwardPressed || !runPressed)){
            animator.SetBool("isRunning", false);
        }
        if (!isrunning && (forwardPressed && runPressed)){
            animator.SetBool("isRunning", true);
        }
        if (isrunning && !isjumping && (forwardPressed && runPressed && jumpPressed)){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);

        }
        if (!iswalking && !isjumping && (!forwardPressed && !runPressed && !jumpPressed)){
            animator.SetBool("isJumping", false);
        }
    }

    public static void jumpingidle(){
        if (!isjumping && jumpPressed){
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidair", true);
        }
        if (isjumping && !jumpPressed){
            animator.SetBool("isJumping", false);
        }
    }
}
