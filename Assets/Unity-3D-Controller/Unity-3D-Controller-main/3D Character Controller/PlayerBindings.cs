using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField] class PlayerBindings : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    float initSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchDamp;
    float xVel;
    float zVel;
    [SerializeField] CharacterController CharacterController;
    [SerializeField] float gravity = 13f;
    Vector3 velocity;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDist = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    [SerializeField] float jumpVelocity = 5f;
	[SerializeField] Transform trans;
    [SerializeField] Image staminaBar;
    public float stamina;
    float maxStamina;
    [SerializeField] float staminaRegenDelay = 2f; // Time to wait before regenerating
    float lastSprintTime;

    void Awake(){
        initSpeed = speed;
        maxStamina = stamina;
    }

    void Update()
    {
		// Movement
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

        //- Sprinting
        if (Input.GetKey(KeyCode.LeftControl) && stamina > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !Input.GetKey(KeyCode.LeftShift)){
            speed = initSpeed + sprintSpeed;
            stamina -= Time.deltaTime;
            lastSprintTime = Time.time;
        }else{
            speed = initSpeed;
            if (Time.time > lastSprintTime + staminaRegenDelay && stamina < maxStamina){
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
                    stamina += Time.deltaTime * maxStamina * 5/100;
                }
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
                    stamina += Time.deltaTime * maxStamina * 1/100;
                }
            }
            if (stamina > maxStamina){
                stamina = maxStamina;
            }
        }

        // Stamina Bar
        staminaBar.fillAmount = stamina / maxStamina;
		
		// Crouching
		if (Input.GetKey(KeyCode.LeftShift)){
			trans.localScale = new Vector3(1,1,1);
            speed = initSpeed - crouchDamp;
		}else{
			trans.localScale = new Vector3(1,1.3f,1);
            speed = initSpeed;
		}
    }
}
