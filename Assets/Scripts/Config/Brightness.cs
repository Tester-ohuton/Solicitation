using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider; // Sliderの参照
    public VolumeProfile profile;
    public float brightness = 5f;

    private SplitToning splitToning; // Bloomエフェクトの参照

    void Start()
    {
        // Global VolumeからSplitToningエフェクトを取得
        profile.TryGet(out splitToning);

        // Sliderの値が変更されたときのイベントを登録
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    // 明るさを変更する関数
    void ChangeBrightness(float newValue)
    {
        // BloomエフェクトのIntensityパラメータを調整する
        splitToning.balance.Override(newValue * brightness); // -30～30が丁度いい    
    }
}
