using UnityEngine;
using System.Collections;

public class DruggedAudioEffect : MonoBehaviour
{
    private AudioReverbFilter reverbFilter;
    public float targetDecay = 20.0f; // High decay for "drugged" feel
    public float duration = 10.0f;

    void Start() {
        reverbFilter = GetComponent<AudioReverbFilter>();
        StartCoroutine(FadeDruggedEffect());
    }

    IEnumerator FadeDruggedEffect() {
        float elapsed = 0;
        float startDecay = reverbFilter.decayTime;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            // Linearly interpolate the decay time over the duration
            reverbFilter.decayTime = Mathf.Lerp(startDecay, targetDecay, elapsed / duration);
            yield return null;
        }
    }
}
