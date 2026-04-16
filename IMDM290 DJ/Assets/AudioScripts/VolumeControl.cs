using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioClip song;

    public Slider volumeSlider;

    private AudioSource audioSource;

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

    void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        //Debug.Log("Manually Changed Volume to: " + audioSource.volume);
    }
}
