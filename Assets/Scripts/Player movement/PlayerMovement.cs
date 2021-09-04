using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //VARIABLES
   [SerializeField] private float moveSpeed;
   [SerializeField] public float walkSpeed;
   [SerializeField] public float runSpeed;
   [SerializeField] public float jumpHeight = 3f;
   private Vector3 moveDirection;
   public CharacterController controller;
   private Animator anim;
   public Transform cam;
   [SerializeField] private float turnSmoothTime = 0.05f;
   private float turnSmoothVelocity;

   Vector3 velocity;
   public float gravity = -9.81f;

   //public Transform groundCheck;
   //public float groundDistance;
   //public LayerMask groundMask;
   public bool isGrounded; 

   private void Start(){
       controller = GetComponent<CharacterController>();
       anim = GetComponentInChildren<Animator>();
   }
   private void Update(){

       //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       if (controller.isGrounded && velocity.y < -10){
           velocity.y = -12;
           isGrounded = true;
       }else{
           isGrounded = false;
       }


       

       float horizontal = Input.GetAxisRaw("Horizontal");
       float vertical = Input.GetAxisRaw("Vertical"); 
       moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
       if(moveDirection != Vector3.zero && !AnimMovementClass.iswalking && !Input.GetKey(KeyCode.LeftShift)){
            playerOrientation();
            AnimMovementClass.animator.SetBool("isWalking", true); 
       }
       else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)){
           //AnimMovementClass.runningAndjumping();
           playerOrientation();
       }
       else if(moveDirection == Vector3.zero){
           //Idle();
       }
       
       if((Input.GetButtonDown("Jump")|| Input.GetButton("Jump")) && controller.isGrounded){
           AnimMovementClass.jumpingidle();
       }
      
       velocity.y += gravity * Time.deltaTime;
       controller.Move(velocity * Time.deltaTime);

   }
   /*private void Idle(){
       anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
   }
   private void Walk(){
       moveSpeed = walkSpeed; 
       anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
   }
   private void Run(){
       moveSpeed = runSpeed;
       anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
   }
   private void Jump(){
       velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
   }   */
   private void playerOrientation(){
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z)
            * Mathf.Rad2Deg
            + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                            targetAngle,
                                            ref turnSmoothVelocity,
                                            turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        return;
   }    
}
