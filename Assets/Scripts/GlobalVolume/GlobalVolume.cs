using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public VolumeProfile volumeProfile;
    private Vignette vignette;
    private SplitToning splitToning;

    private void Start()
    {
        // VolumeProfileが設定されていない場合は新しいVolumeProfileを作成
        if (volumeProfile == null)
            volumeProfile = gameObject.AddComponent<Volume>().profile;

        // Vignetteを取得
        volumeProfile.TryGet(out vignette);
        volumeProfile.TryGet(out splitToning);
    }

    public void SetSplitToningIntensity(float intensity)
    {
        splitToning.balance.value = intensity;
    }

    public void SetSplitToningColor(Color color)
    {
        splitToning.shadows.value = color;
    }

    public void ShowIllumination()
    {
        DOTween.To(() => splitToning.balance.value, x => splitToning.balance.value = x, 100f, 1f);

        SetColor(Color.white);
    }

    // VignetteのIntensityを指定された値に設定する
    public void SetVignetteIntensity(float intensity)
    {
        vignette.intensity.value = intensity;
    }

    // Colorを指定された色に設定する
    public void SetColor(Color color)
    {
        vignette.color.value = color;
    }

    // アイテム取得時の表現
    public void ShowItemObtainedEffect()
    {
        // VignetteのIntensityを0.5fにTween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.4f, 1f)
            .OnComplete(() =>
            {
                // 1秒後にVignetteのIntensityを0fにTween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f);
            });

        SetColor(Color.yellow);
    }

    // 警告時の表現
    public void ShowWarningEffect()
    {
        // VignetteのIntensityを0.5fにTween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.4f, 1f)
            .OnComplete(() =>
            {
                // 1秒後にVignetteのIntensityを0fにTween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f);
            });

        SetColor(Color.red);
    }

    // 回復時の表現
    public void ShowRecoveryEffect()
    {
        // VignetteのIntensityを0.5fにTween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.4f, 1f)
            .OnComplete(() =>
            {
                // 1秒後にVignetteのIntensityを0fにTween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f);
            });

        SetColor(Color.green);
    }
}