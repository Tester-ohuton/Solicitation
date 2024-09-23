using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }

    public void StopBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = 0;
        bgmAudioSource.Stop();
    }


    public void PlaySE(SESoundData.SE se, Vector3 position)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        AudioSource.PlayClipAtPoint(data.audioClip, position);
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Game,
        End1,
        End2,
        Result,
        Dungeon,
        WARNING,// ���ꂪ���x���ɂȂ�
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 0.2f)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Dead,
        ItemGet,
        Comp,
        Goal,
        CAVEAT,// ���ꂪ���x���ɂȂ�
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 0.2f)]
    public float volume = 1;
}
