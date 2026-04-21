using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField]public MediaPipeBodyTracker mediapipe;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Setting slider value to LEFT HAND Y position
        slider.value = Mathf.Abs(mediapipe.pendingLeftHandPosition.y) ;
        Debug.Log($"Left hand y: {mediapipe.pendingLeftHandPosition.y}");
        if(mediapipe.pendingLeftPinch)
        {
            Debug.Log("Pinch!!");
        }
    }
}
