using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtra : MonoBehaviour
{
    //Variables
    [SerializeField] Animator camShake;
	public LayerMask groundMask;
    public float groundDist = 0.4f;
    private bool isGrounded;
    public Transform groundCheck;
    public bool handFull;
    [SerializeField] AudioSource conSteps;
    [SerializeField] Transform cam;
    [SerializeField] float pickUpDist;
    [SerializeField] LayerMask pickLayerMask;
    [SerializeField] Transform itemHolder;
    [SerializeField] GameObject Pickup_Txt;

    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position,groundDist,groundMask);
		
        if ((Input.GetButton("Vertical") | Input.GetButton("Horizontal")) && isGrounded){
            camShake.SetBool("move?", true);
            while (!conSteps.isPlaying){
            conSteps.Play();
        }
        }else{
			camShake.SetBool("move?", false);
            conSteps.Stop();
		}

        if (Input.GetKeyDown(KeyCode.E)){
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit raycastHit, pickUpDist, pickLayerMask)){
                // Check if the hit object has a parent
                if (raycastHit.transform.parent != null)
                {
                    // Try to get the Grabbable component from the parent
                    Grabbable grabbable = raycastHit.transform.parent.GetComponent<Grabbable>();

                    if (grabbable != null && !handFull)
                    {
                        grabbable.Grab(itemHolder);
                        handFull = true;
                    }
                }
                else
                {
                    // If no parent, check the hit object itself
                    Grabbable grabbable = raycastHit.transform.GetComponent<Grabbable>();

                    if (grabbable != null && !handFull)
                    {
                        grabbable.Grab(itemHolder);
                        handFull = true;
                    }
                }
            }
        }





        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit rHit, pickUpDist, pickLayerMask)){
            // Check if the hit object has a parent
            if (rHit.transform.parent != null)
            {
                // Try to get the Grabbable component from the parent
                Grabbable grabbable = rHit.transform.parent.GetComponent<Grabbable>();

                if (grabbable != null && !handFull)
                {
                    Pickup_Txt.SetActive(true);
                }
            }
            else
            {
                // If no parent, check the hit object itself
                Grabbable grabbable = rHit.transform.GetComponent<Grabbable>();

                if (grabbable != null && !handFull)
                {
                   Pickup_Txt.SetActive(true);
                }
            }
        }else{
            Pickup_Txt.SetActive(false);
        }
    }
}