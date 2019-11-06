using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class sanityValue : MonoBehaviour
{
    public Slider sanitySlider;
    public float sanity;
    private void Start()
    {
        sanitySlider = GetComponent<Slider>();
        sanitySlider.value = sanity;
    }
}
