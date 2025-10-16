using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] BGMSoundData.BGM titleBGM;
    [SerializeField] BGMSoundData.BGM gameBGM;

    public bool isTitle;
    public bool isGame;

    void Start()
    {
        if (isTitle)
        {
            SoundManager.Instance.PlayBGM(titleBGM);
        }
        else if (isGame)
        {
            SoundManager.Instance.PlayBGM(gameBGM);
        }
    }
}
