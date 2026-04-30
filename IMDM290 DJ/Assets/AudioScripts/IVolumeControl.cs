using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IVolumeControl : MonoBehaviour
{
    public MediaPipeBodyTracker mediaPipe;
    public DJController dj;

    public Slider volumeSlider;

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

            if (volumeSlider)
            {
                volumeSlider.fillRect.GetComponent<Image>().color = canControl ? Color.cyan : Color.white;
            }
        }

        if (canControl && mediaPipe != null && mediaPipe.RightHandTracked)
        {
            float handX = Mathf.Clamp01(mediaPipe.RightHandPosition.x);

            float targetVolume = Mathf.Lerp(minVolume, maxVolume, handX);

            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * smoothSpeed);

            if (volumeSlider)
            {
                volumeSlider.value = handX;
            }
        }
    }
}