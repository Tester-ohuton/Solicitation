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
        //SoundManager.Instance.PlayBGM(titleBGM);
    }

    private void Update()
    {
        if(isTitle)
        {
            SoundManager.Instance.PlayBGM(titleBGM);
            isTitle = false;
        }
        else if(isGame)
        {
            SoundManager.Instance.PlayBGM(gameBGM);
            isGame = false;
        }
        else if(isEND1)
        {
            SoundManager.Instance.PlayBGM(end1_BGM);
            isEND1 = false;
        }
        else if(isEND2)
        {
            SoundManager.Instance.PlayBGM(end2_BGM);
            isEND2 = false;
        }
        else if(isResult)
        {
            SoundManager.Instance.PlayBGM(resultBGM);
            isResult = false;
        }
    }

    public void StopBGM()
    {
        SoundManager.Instance.StopBGM(titleBGM);
        SoundManager.Instance.StopBGM(gameBGM);
        SoundManager.Instance.StopBGM(end1_BGM);
        SoundManager.Instance.StopBGM(end2_BGM);
        SoundManager.Instance.StopBGM(resultBGM);
    }
}
