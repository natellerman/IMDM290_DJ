using UnityEngine;

public class Change_Pitch : MonoBehaviour
{
    public float pitchValue = 1.0f;
    public AudioClip song;
    
    private AudioSource audioSource;
    // Upper and lower bounds for pitch
    private float low = 0.75f;
    private float high = 1.25f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting music
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;
        audioSource.loop = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}