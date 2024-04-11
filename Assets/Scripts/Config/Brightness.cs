using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Brightness : MonoBehaviour
{
    public Light mainLight; // Main Lightの参照
    public Slider brightnessSlider; // Sliderの参照
    public VolumeProfile volumeProfile;
    public GlobalVolume globalVolume; // Global Volumeの参照
    
    private SplitToning splitToning; // Bloomエフェクトの参照

    void Start()
    {
        // Global VolumeからSplitToningエフェクトを取得
        globalVolume.volumeProfile.TryGet(out splitToning);
        // Sliderの初期値を設定
        brightnessSlider.value = mainLight.intensity;
        // Sliderの値が変更されたときのイベントを登録
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    // 明るさを変更する関数
    void ChangeBrightness(float newValue)
    {
        mainLight.intensity = newValue;

        // BloomエフェクトのIntensityパラメータを調整する
        splitToning.balance.Override(newValue * 5); // 5は適切な倍率ですが必要に応じて調整してください
    }
}
