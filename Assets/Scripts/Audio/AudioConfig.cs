using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioConfig : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null) return;

        // スライダーを触ったら音量が変化する
        volumeSlider.onValueChanged.AddListener((value)=>
        {
            value = Mathf.Clamp01(value);

            // 変化するのは-80～0までの間
            float decibel = 20f * Mathf.Log10(value);
            decibel = Mathf.Clamp(decibel, -80f, 0f);
            audioMixer.SetFloat("BGM", decibel);
            audioMixer.SetFloat("SE", decibel);
        });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}