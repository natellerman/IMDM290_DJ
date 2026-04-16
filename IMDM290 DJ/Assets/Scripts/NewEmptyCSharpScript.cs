using UnityEngine;

public class ReverbController : MonoBehaviour {
    public AudioReverbZone reverbZone;

    void Start() {
        // Change preset via code
        reverbZone.reverbPreset = AudioReverbPreset.Cave;
    }
}
