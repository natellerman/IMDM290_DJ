using UnityEngine;
using UnityEngine.InputSystem;

public class IVolumeControl : MonoBehaviour
{
    public MediaPipeBodyTracker mediaPipe;
    public DJController dj;

    public float minVolume = 0f;
    public float maxVolume = 1f;

    public float smoothSpeed = 5f;

    private bool canControl = false;

    void Update()
    {
        AudioSource audioSource = dj.GetActiveDeck();

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            canControl = !canControl;
        }

        if (canControl && mediaPipe != null && mediaPipe.RightHandTracked)
        {
            float handX = Mathf.Clamp01(mediaPipe.RightHandPosition.x);

            float targetVolume = Mathf.Lerp(minVolume, maxVolume, handX);

            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * smoothSpeed);
        }
    }
}