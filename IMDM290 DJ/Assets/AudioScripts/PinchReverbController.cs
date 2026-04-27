using UnityEngine;
using UnityEngine.InputSystem; // Input System
using UnityEngine.UI;

public class PinchReverbController : MonoBehaviour
{
    public AudioReverbFilter reverbFilter;

    // MediaPipe script
    public MediaPipeBodyTracker mediaPipe;

    public Slider reverbSlider;

    [Header("Reverb Settings")]
    public float minDecay = 1.0f;
    public float maxDecay = 20.0f;

    [Header("Pinch Settings")]
    public float minPinchDistance = 0.01f;
    public float maxPinchDistance = 0.1f;

    [Header("Smoothing")]
    public float smoothSpeed = 5f;

    private bool isActive = false;
    private float currentDecay;

    void Update()
    {

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            isActive = !isActive;

            if (reverbSlider)
                reverbSlider.fillRect.GetComponent<Image>().color = isActive ? Color.magenta : Color.white;
        }

        if (Keyboard.current.dKey.wasReleasedThisFrame)
                    isActive = false;

        if (isActive && mediaPipe != null && mediaPipe.RightHandTracked)
        {
            float pinchDistance = mediaPipe.RightThumbIndexDistance;

            // Normalize pinch
            float t = Mathf.InverseLerp(minPinchDistance, maxPinchDistance, pinchDistance);

            // Invert: closer = more reverb
            float targetDecay = Mathf.Lerp(maxDecay, minDecay, t);

            // Smooth it (prevents jitter)
            currentDecay = Mathf.Lerp(currentDecay, targetDecay, Time.deltaTime * smoothSpeed);

            reverbFilter.decayTime = currentDecay;

            if (reverbSlider) reverbSlider.value = 1f - t;
        }
    }
}