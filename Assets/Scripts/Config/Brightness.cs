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
    public VolumeProfile profile;
    
    private SplitToning splitToning; // Bloomエフェクトの参照
    // private ColorAdjustments colorAdjustments; // ColorAdjustmentsの参照

    void Start()
    {
        // Global VolumeからSplitToningエフェクトを取得
        profile.TryGet(out splitToning);
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
        splitToning.balance.Override(newValue * 5); // -30～30が丁度いい
        // colorAdjustments.postExposure.Override(newValue * 5); // 白飛びしたり真っ黒になる
    }
}
