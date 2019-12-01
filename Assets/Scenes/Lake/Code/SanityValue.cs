using UnityEngine;
using UnityEngine.UI;

public class SanityValue : MonoBehaviour {
    public float sanity;
    public Slider sanitySlider;

    private void Start() {
        sanitySlider = GetComponent<Slider>();
        sanitySlider.value = sanity; // sets slider value to pre-determined amount
    }
}