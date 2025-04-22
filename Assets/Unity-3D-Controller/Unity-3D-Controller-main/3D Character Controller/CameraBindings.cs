using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraBindings : MonoBehaviour
{
    public float mouseSensitivity = 80f;
    [SerializeField] Transform playerBody;
    private float xRot;
    [SerializeField] Slider camSensitivitySlider;


    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot,-90,90);
        transform.localRotation = Quaternion.Euler(xRot,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
        mouseSensitivity = camSensitivitySlider.value;
    }

    public void ResetCamSensitivity(){
        camSensitivitySlider.value = 150;
    }
}
