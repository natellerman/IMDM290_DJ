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
            Debug.Log("Manually Changed Volume to: " + audioSource.volume);
        }
    }

        void ChangeVolume()
        {
            audioSource.volume = volumeSlider.value;
            Debug.Log("Manually Changed Volume to: " + audioSource.volume);
        }

    // // Update is called once per frame
    // void Update()
    //     {
    //         if (volumeSlider != null && audioSource != null)
    //         {
    //             audioSource.volume = volumeSlider.value;
    //             // This will print the volume to the console so you can see if it's changing
    //             Debug.Log("Current Volume: " + audioSource.volume);
    //         }
    //     }
}
