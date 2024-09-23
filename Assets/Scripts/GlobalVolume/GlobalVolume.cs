using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

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
    public Slider volumeSlider;

    private Vignette vignette;
    private SplitToning splitToning;

    private void Start()
    {
        // VolumeProfile���ݒ肳��Ă��Ȃ��ꍇ�͐V����VolumeProfile���쐬
        if (volumeProfile == null)
            volumeProfile = gameObject.AddComponent<Volume>().profile;

        // Vignette���擾
        volumeProfile.TryGet(out vignette);
        volumeProfile.TryGet(out splitToning);

        if (volumeSlider == null) return;

        // �X���C�_�[��G�����特�ʂ��ω�����
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            SetSplitToningIntensity(value);
            SetSplitToningColor(Color.white);
        });
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
        DOTween.To(() => splitToning.balance.value, x => splitToning.balance.value = x, -100f, 1f)
            .OnComplete(() =>
            {
                // 1�b���Vignette��Intensity��0f��Tween
                DOTween.To(() => splitToning.balance.value, x => splitToning.balance.value = x, 0f, 1f);
            });

        SetColor(Color.white);
    }

    // Vignette��Intensity���w�肳�ꂽ�l�ɐݒ肷��
    public void SetVignetteIntensity(float intensity)
    {
        vignette.intensity.value = intensity;
    }

    // Color���w�肳�ꂽ�F�ɐݒ肷��
    public void SetColor(Color color)
    {
        vignette.color.value = color;
    }

    // �A�C�e���擾���̕\��
    public void ShowItemObtainedEffect(float vine, float duration)
    {
        // Vignette��Intensity��0.5f��Tween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, vine, 1f)
            .OnComplete(() =>
            {
                // 1�b���Vignette��Intensity��0f��Tween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, duration);
            });

        SetColor(Color.black);
    }

    public void ShowOpenEntranceEffect(float vine, float duration)
    {
        // Vignette��Intensity��0.5f��Tween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, vine, duration);

        SetColor(Color.black);
    }

    // �x�����̕\��
    public void ShowWarningEffect()
    {
        // Vignette��Intensity��0.5f��Tween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.4f, 1f)
            .OnComplete(() =>
            {
                // 1�b���Vignette��Intensity��0f��Tween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f);
            });

        SetColor(Color.red);
    }

    // �񕜎��̕\��
    public void ShowRecoveryEffect()
    {
        // Vignette��Intensity��0.5f��Tween
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.4f, 1f)
            .OnComplete(() =>
            {
                // 1�b���Vignette��Intensity��0f��Tween
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f);
            });

        SetColor(Color.green);
    }
}