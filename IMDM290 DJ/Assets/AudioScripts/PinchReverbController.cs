using UnityEngine;
using UnityEngine.InputSystem;

public class PinchReverbController : MonoBehaviour
{
    public MediaPipeBodyTracker mediaPipe;
    public DJController dj;

    public float minDecay = 1.0f;
    public float maxDecay = 20.0f;

    public float minPinchDistance = 0.01f;
    public float maxPinchDistance = 0.1f;

    public float smoothSpeed = 5f;

    private bool isActive = false;
    private float currentDecay;

    void Update()
    {
        AudioSource audioSource = dj.GetActiveDeck();
        AudioReverbFilter reverb = audioSource.GetComponent<AudioReverbFilter>();

        if (Keyboard.current.dKey.wasPressedThisFrame)
            isActive = true;

        if (Keyboard.current.dKey.wasReleasedThisFrame)
            isActive = false;

        if (isActive && mediaPipe != null && mediaPipe.RightHandTracked && reverb != null)
        {
            float pinchDistance = mediaPipe.RightThumbIndexDistance;

            float t = Mathf.InverseLerp(minPinchDistance, maxPinchDistance, pinchDistance);

            float targetDecay = Mathf.Lerp(maxDecay, minDecay, t);

            currentDecay = Mathf.Lerp(currentDecay, targetDecay, Time.deltaTime * smoothSpeed);

            reverb.decayTime = currentDecay;
        }
    }
}