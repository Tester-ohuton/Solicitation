using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTest : MonoBehaviour
{
    private float mouseSensitivity = 1f;    //マウス感度
    public Slider mouseSlider;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = GameManager.instance.mouseSensitivity;

        if (mouseSlider == null) return;

        mouseSlider.value = mouseSensitivity;

        mouseSlider.onValueChanged.AddListener((value) =>
        {
            SetMouseSencitivity(value);
        });
    }

    public void SetMouseSencitivity(float value)
    {
        mouseSensitivity = value;
        GameManager.instance.mouseSensitivity = mouseSensitivity;
    }

    public float GetMouseSensitivity()
    {
        return mouseSensitivity;
    }
}
