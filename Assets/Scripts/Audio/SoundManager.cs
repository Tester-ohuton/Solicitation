using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource bgmAudioSource;   // BGM用
    [SerializeField] AudioSource seAudioSource;    // 2D SE用

    [Header("Sound Data Lists")]
    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float bgmMasterVolume = 1f;
    [Range(0f, 1f)] public float seMasterVolume = 1f;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも維持
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =======================
    // 🎵 BGM 再生・停止
    // =======================

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(d => d.bgm == bgm);
        if (data == null || data.audioClip == null) return;

        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    // =======================
    // 🔊 2D効果音（UI・メニュー用）
    // =======================

    public void PlaySE2D(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(d => d.se == se);
        if (data == null || data.audioClip == null) return;

        seAudioSource.clip = data.audioClip;
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.spatialBlend = 0f; // 2D再生
        seAudioSource.Play();
    }

    // =======================
    // 🌍 3D効果音（衝突・爆発・環境音など）
    // =======================

    public void PlaySE3D(SESoundData.SE se, Vector3 position)
    {
        SESoundData data = seSoundDatas.Find(d => d.se == se);
        if (data == null || data.audioClip == null) return;

        GameObject tempGO = new GameObject("3D_SE_" + se.ToString());
        tempGO.transform.position = position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = data.audioClip;
        aSource.volume = data.volume * seMasterVolume * masterVolume;

        // 🎧 3Dサウンド設定
        aSource.spatialBlend = 1.0f; // 完全3D音
        aSource.rolloffMode = AudioRolloffMode.Logarithmic;
        aSource.minDistance = 5f;     // 近距離ではフル音量
        aSource.maxDistance = 50f;    // 50m以上でほぼ無音
        aSource.dopplerLevel = 0.5f;  // 動きによる音の変化

        aSource.Play();
        Destroy(tempGO, data.audioClip.length + 0.1f);
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Stage,
        Result,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Dead,
        ItemGet,
        Goal,
        Warning,     // ← CAVEAT の代わり（最も自然）
        Alert,       // ← 緊急度を出したいならこっち
        Explosion,
        Click,
        LevelUp,
        DoorOpen,
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}
