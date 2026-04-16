using UnityEngine;
using UnityEngine.InputSystem;

public class Keyboard_Input : MonoBehaviour
{
    public VolumeControl volumeScript;

    private bool isVolumeMode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            isVolumeMode = !isVolumeMode;
        }

        // 2. Volume Controls
        if (isVolumeMode)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                volumeScript.AdjustVolume(-1f); // Pass -1 to go down
            }
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                volumeScript.AdjustVolume(1f);  // Pass 1 to go up
            }
        }
        else
        {
            if(Keyboard.current.leftArrowKey.isPressed)
            {
            
            }
        
            else if(Keyboard.current.rightArrowKey.isPressed)
            {

            }
        }
        
        
    }
}