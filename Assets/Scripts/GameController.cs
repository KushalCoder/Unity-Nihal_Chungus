using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private bool _isPaused;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Slider gammaSlider;

    // Post Process
    [SerializeField] PostProcessProfile profile;
    [Range(-2f, 2f)] // Add a reasonable range for the Lift value
    [SerializeField] float gammaValue = 0f;
    private ColorGrading colorGrading;

    void Awake(){
        Unpause();
    }

    void Start(){
        if (profile == null)
        {
            Debug.LogError("Post Process Profile is not assigned!");
            enabled = false;
            return;
        }

        colorGrading = profile.GetSetting<ColorGrading>();
        if (colorGrading == null)
        {
            Debug.LogError("Color Grading settings not found in the Post Process Profile!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Pausing/Unpausing
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (_isPaused){
                Unpause();
            }else{
                Pause();
            }
        }

        // gamma
        if (colorGrading != null)
        {
            // Get the gamma value set in inspector
            Vector4 inspGamma = colorGrading.gamma.value;
            // Create a new Vector4 for the gamma value
            Vector4 newGamma = new Vector4(inspGamma.x, inspGamma.y, inspGamma.z, gammaValue);

            // Set the gamma value
            colorGrading.gamma.value = newGamma;
        }
        gammaValue = gammaSlider.value;
    }

    public void Pause(){
        Time.timeScale = 0.00000000000000000001f;
        _isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        PausePanel.SetActive(true);
    }

    public void Unpause(){
        Time.timeScale = 1.0f;
        _isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        PausePanel.SetActive(false);
    }

    public void ResetGamma(){
        gammaSlider.value = 0.4f;
    }
}
