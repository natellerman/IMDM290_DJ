using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField]public MediaPipeBodyTracker mediapipe;
    public Slider slider;
    // If pitchSlider, we'll track the Y values
    public bool pitchSlider;
    public float maxSlidervalue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Setting slider
        if(pitchSlider)
        {
            slider.value = Mathf.Abs(maxSlidervalue - mediapipe.pendingLeftHandPosition.y) ;
            Debug.Log($"Left hand y: {mediapipe.pendingLeftHandPosition.y}");
        }
        else
        {
            slider.value = Mathf.Abs(maxSlidervalue - mediapipe.pendingLeftHandPosition.x) ;
            Debug.Log($"Left hand x: {mediapipe.pendingLeftHandPosition.x}");
        }
       
        if(mediapipe.pendingLeftPinch)
        {
            Debug.Log("Pinch!!");
        }
    }
}
