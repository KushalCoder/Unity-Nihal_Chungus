using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Rigidbody objectRb;
    private Transform itemHolder;
    [SerializeField] Camera mainCamera;
    private float moveSpeed = 5f;
    private Vector3 orScale;
    [SerializeField] PlayerExtra pExtra;

    void Awake(){
        objectRb = GetComponent<Rigidbody>();
        orScale = transform.localScale;
    }

    public void Grab(Transform itemHolder){
        this.itemHolder = itemHolder;
        transform.localScale = orScale;
    }

    void FixedUpdate(){
        if (itemHolder != null){
            transform.SetParent(itemHolder);
            objectRb.useGravity = false;
            objectRb.isKinematic = true;
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }else{
            transform.SetParent(null);
            objectRb.useGravity = true;
            objectRb.isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && itemHolder != null){
            itemHolder = null;
            transform.localScale = orScale;

            Vector3 throwDirection = mainCamera.transform.forward; // Get the camera's forward direction
            objectRb.isKinematic = false;
            objectRb.velocity = throwDirection * moveSpeed; // Set the object's velocity
            pExtra.handFull = false;
        }
    }
}
