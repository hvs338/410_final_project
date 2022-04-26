using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotor : MonoBehaviour
{   
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float gravity = -9.8f;
    private bool isGrounded;
    public float jumpHeight = 3f;

    public float crouchTimer;
    public bool crouching;
    public bool lerpCrouching;
    public bool sprinting;
    


    // Start is called before the first frame update
    void Start()
    {
     controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
     isGrounded = controller.isGrounded;
     if(lerpCrouching){
         crouchTimer += Time.deltaTime;
         float p = crouchTimer/1;
         p *=p;
         
         if(crouching)
             controller.height = Mathf.Lerp(controller.height,1,p);

         
         else
            controller.height = Mathf.Lerp(controller.height,2,p);
        
        if(p>1){
            lerpCrouching = false;
            crouchTimer = 0f;
        }
     }   
    }

    public void ProcessMove(Vector2 input){
       Vector3 moveDirection = Vector3.zero;
       moveDirection.x = input.x;
       moveDirection.z = input.y;
       controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime); 
       if (isGrounded && playerVelocity.y < 0){
           playerVelocity.y = -2f;
       }
        playerVelocity.y += gravity*Time.deltaTime;
        controller.Move(playerVelocity*Time.deltaTime);
    }

    public void Jump(){

        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight*-3.0f*gravity);
        }

    }

    public void Crouch(){
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouching = true;

    }

    public void Sprint(){
        sprinting = !sprinting;
        if(sprinting)
            speed = 8;
        else
            speed = 5;

    }
}
