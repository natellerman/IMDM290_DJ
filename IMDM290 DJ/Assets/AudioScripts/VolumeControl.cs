using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VolumeControl : MonoBehaviour
{
    public AudioClip song;
    public Slider volumeSlider;
    private AudioSource audioSource;

    private bool canControlVolume = false;

    public float changeSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });

        audioSource.clip = song;
        audioSource.loop = true;
        audioSource.Play();

        volumeSlider.value = audioSource.volume;

        void ChangeVolume()
        {
            audioSource.volume = volumeSlider.value;
            //Debug.Log("Manually Changed Volume to: " + audioSource.volume);
        }
    }

    public void AdjustVolume(float direction)
    {
        if (volumeSlider != null)
        {
            // direction will be -1 (Down) or 1 (Up)
            volumeSlider.value += direction * changeSpeed * Time.deltaTime;
        }
    }

    void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        //Debug.Log("Manually Changed Volume to: " + audioSource.volume);
    }
}