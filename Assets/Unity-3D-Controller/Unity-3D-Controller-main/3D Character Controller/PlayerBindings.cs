using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBindings : MonoBehaviour
{
    public float speed = 30f;
    float xVel;
    float zVel;
    public CharacterController CharacterController;
    public float gravity = 13f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDist = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpVelocity = 5f;
	[SerializeField] Transform trans;	

    void Update()
    {
		//Main
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDist,groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded){
            velocity.y = jumpVelocity;
        }

        xVel = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        zVel = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 move = transform.right * xVel + transform.forward * zVel;
        CharacterController.Move(move * speed * Time.deltaTime);
        velocity.y -= gravity * Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);
		
		//Else
		if (Input.GetKey(KeyCode.LeftShift)){
			trans.localScale = new Vector3(1,1,1);
		}else{
			trans.localScale = new Vector3(1,1.3f,1);
		}
    }
}
