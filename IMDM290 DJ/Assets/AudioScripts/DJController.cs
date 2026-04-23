using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DJController : MonoBehaviour
{
    public AudioSource deckA;
    public AudioSource deckB;

    public float transitionDuration = 3f;

    private bool isPlayingA = true;
    private bool isTransitioning = false;

    void Start()
    {
        deckA.volume = 1f;
        deckB.volume = 0f;

        deckA.Play();
        deckB.Play();
    }

    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame && !isTransitioning)
        {
            StartCoroutine(Crossfade());
        }
    }

    IEnumerator Crossfade()
    {
        isTransitioning = true;

        float time = 0f;

        AudioSource from = isPlayingA ? deckA : deckB;
        AudioSource to = isPlayingA ? deckB : deckA;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            float t = time / transitionDuration;

            from.volume = Mathf.Lerp(1f, 0f, t);
            to.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        isPlayingA = !isPlayingA;
        isTransitioning = false;
    }

    public AudioSource GetActiveDeck()
    {
        return isPlayingA ? deckA : deckB;
    }
}