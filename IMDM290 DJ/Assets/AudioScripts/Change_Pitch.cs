using UnityEngine;
using UnityEngine.UI;

public class Change_Pitch : MonoBehaviour
{
    public float pitchValue = 1.0f;
    public AudioClip song;
    public Slider slider;
    public Slider reverse;
    
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting music
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;
        audioSource.loop = true;


    }

    // Setting the pitch to the value of the slider
    void Update()
    {
        audioSource.pitch = slider.value * reverse.value;
        
    }


}