using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider; // Slider�̎Q��
    public VolumeProfile profile;
    public float brightness = 5f;

    private SplitToning splitToning; // Bloom�G�t�F�N�g�̎Q��

    void Start()
    {
        // Global Volume����SplitToning�G�t�F�N�g���擾
        profile.TryGet(out splitToning);

        // Slider�̒l���ύX���ꂽ�Ƃ��̃C�x���g��o�^
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    // ���邳��ύX����֐�
    void ChangeBrightness(float newValue)
    {
        // Bloom�G�t�F�N�g��Intensity�p�����[�^�𒲐�����
        splitToning.balance.Override(newValue * brightness); // -30�`30�����x����    
    }
}
