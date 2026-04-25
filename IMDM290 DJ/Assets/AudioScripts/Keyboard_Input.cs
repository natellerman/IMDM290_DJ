using UnityEngine;
using UnityEngine.InputSystem;

public class Keyboard_Input : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Showing tutorial
        if(Keyboard.current.eKey.isPressed)
        {
            if(tutorial.activeSelf == false)
            {
                tutorial.SetActive(true);
            }
            else
            {
                tutorial.SetActive(false);
            }

        }
        
        
    }
}