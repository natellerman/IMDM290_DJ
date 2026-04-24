using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IChange_Pitch : MonoBehaviour
{
    public MediaPipeBodyTracker mediaPipe;
    public DJController dj;

    public Slider pitchSlider;

    [Header("Pitch Settings")]
    public float minPitch = 0.5f;
    public float maxPitch = 2.0f;

    [Header("Hand Height Range")]
    public float minY = 0.2f;
    public float maxY = 0.8f;

    [Header("Smoothing")]
    public float smoothSpeed = 5f;

    private bool isActive = false;
    private float currentPitch = 1f;

    void Update()
    {
        AudioSource audioSource = dj.GetActiveDeck();

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            isActive = !isActive;

            if (pitchSlider)
                pitchSlider.fillRect.GetComponent<Image>().color = isActive ? Color.yellow : Color.white;
        }

        if (isActive && mediaPipe != null && mediaPipe.RightHandTracked)
        {
            float handY = mediaPipe.RightHandPosition.y;

            float t = Mathf.InverseLerp(minY, maxY, handY);
            float targetPitch = Mathf.Lerp(minPitch, maxPitch, t);

            currentPitch = Mathf.Lerp(currentPitch, targetPitch, Time.deltaTime * smoothSpeed);

            audioSource.pitch = currentPitch;

            if (pitchSlider)
            {
                pitchSlider.value = t;
            }
        }
    }
}