using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Image illuminanceImage;

    void Start()
    {
        brightnessSlider = GetComponent<Slider>();
        brightnessSlider.onValueChanged.AddListener(SetBrightness);

        // Initialize the brightness to the current slider value
        SetBrightness(brightnessSlider.value);
    }

    void SetBrightness(float value)
    {
        if (illuminanceImage != null)
        {
            Color color = illuminanceImage.color;
            color.a = value; // Assuming brightness is controlled by alpha channel
            illuminanceImage.color = color;
        }
    }
}
