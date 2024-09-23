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

        // �X���C�_�[��G�����特�ʂ��ω�����
        volumeSlider.onValueChanged.AddListener((value)=>
        {
            value = Mathf.Clamp01(value);

            // �ω�����̂�-80�`0�܂ł̊�
            float decibel = 20f * Mathf.Log10(value);
            decibel = Mathf.Clamp(decibel, -80f, 0f);
            audioMixer.SetFloat("BGM", decibel);
            audioMixer.SetFloat("SE", decibel);
            audioMixer.SetFloat("Master", decibel);
        });
    }
}