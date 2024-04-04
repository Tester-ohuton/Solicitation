using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] BGMSoundData.BGM bgm;

    void Start()
    {
        SoundManager.Instance.PlayBGM(bgm);
    }
}
