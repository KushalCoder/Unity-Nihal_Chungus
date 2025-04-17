using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPan : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    void Update()
    {
        float panValue = Mathf.Sin(Time.time) * 0.5f; // Creates a panning effect between -0.5 and 0.5
        bgm.panStereo = panValue;
    }
}
