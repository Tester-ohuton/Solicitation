using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    private Image image;

    void Start()
    {
        brightnessSlider = GetComponent<Slider>();
        brightnessSlider.onValueChanged.AddListener(SetBrightness);

        image = GetComponent<Image>();

        // Initialize the brightness to the current slider value
        SetBrightness(brightnessSlider.value);
    }

    void SetBrightness(float value)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = value; // Assuming brightness is controlled by alpha channel
            image.color = color;
        }
    }
}
