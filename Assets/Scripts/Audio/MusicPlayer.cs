using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] BGMSoundData.BGM titleBGM;
    [SerializeField] BGMSoundData.BGM gameBGM;
    [SerializeField] BGMSoundData.BGM end1_BGM;
    [SerializeField] BGMSoundData.BGM end2_BGM;
    [SerializeField] BGMSoundData.BGM resultBGM;

    public bool isTitle;
    public bool isGame;
    public bool isEND1;
    public bool isEND2;
    public bool isResult;

    void Start()
    {
        SoundManager.Instance.PlayBGM(titleBGM);
    }

    private void Update()
    {
        if(!isTitle)
        {
            StopBGM();
        }

        if(isGame)
        {
            StopBGM();
            SoundManager.Instance.PlayBGM(gameBGM);
        }

        if(isEND1)
        {
            StopBGM();
            SoundManager.Instance.PlayBGM(end1_BGM);
        }

        if(isEND2)
        {
            StopBGM();
            SoundManager.Instance.PlayBGM(end2_BGM);
        }

        if(isResult)
        {
            StopBGM();
            SoundManager.Instance.PlayBGM(resultBGM);
        }
    }

    private void StopBGM()
    {
        SoundManager.Instance.StopBGM(titleBGM);
        SoundManager.Instance.StopBGM(gameBGM);
        SoundManager.Instance.StopBGM(end1_BGM);
        SoundManager.Instance.StopBGM(end2_BGM);
        SoundManager.Instance.StopBGM(resultBGM);
    }
}
