using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolicitationDirector : MonoBehaviour
{
    // ResultPanel
    [SerializeField] GameObject resultPanel;

    // SetumeiPanel
    [SerializeField] GameObject setumeiPanel;

    // titlePanel
    [SerializeField] GameObject titlePanel;

    // SavePanel
    [SerializeField] GameObject savePanel;

    // OptionPanel
    [SerializeField] GameObject optionPanel;

    // QuitPanel
    [SerializeField] GameObject quitPanel;

    [Header("CountDownTimer")]
    [SerializeField] CountDownTimer countDownTimer;

    // ゲームモード
    enum GameMode
    {
        Prologue,
        Title,
        Game,
        End1,
        End2,
        Result,
    }

    GameMode gameMode;

    private enum SceneTimer
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    void Start()
    {
        Time.timeScale = 1;

        titlePanel.SetActive(true);

        setumeiPanel.SetActive(false);
        resultPanel.SetActive(false);
        savePanel.SetActive(false);
        optionPanel.SetActive(false);
        quitPanel.SetActive(false);

        // 最初のモード
        gameMode = GameMode.Prologue;
    }

    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Prologue:
                PrologueMode();
                break;
            case GameMode.Title:
                TitleMode();
                break;
            case GameMode.Game:
                GamingMode();
                break;
            case GameMode.End1:
                End1Mode();
                break;
            case GameMode.End2:
                End2Mode();
                break;
            case GameMode.Result:
                ResultMode();
                break;
            default:
                break;
        }
    }

    private void GameResult()
    {
        resultPanel.SetActive(true);
        enabled = false; // Stop updating
    }

    void Setumei()
    {
        setumeiPanel.SetActive(!setumeiPanel.activeSelf);
        SceneFlagManager.Instance.isPlayerMoving = !setumeiPanel.activeSelf;
    }

    void PrologueMode()
    {
        gameMode = GameMode.Title;
        Debug.Log(gameMode);
    }

    void TitleMode()
    {
        //Debug.Log(gameMode);
    }

    public void TitleButton()
    {
        titlePanel.SetActive(!titlePanel.activeSelf);
        SceneFlagManager.Instance.isPlayerMoving = !titlePanel.activeSelf;
        gameMode = GameMode.Game;
    }

    public void QuitButton()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
    }

    public void QuitPanel()
    {
        Application.Quit();
    }

    void GamingMode()
    {
        if(ItemText.instance.isComp)  // 段ボールを全て開けたら
        {
            countDownTimer.TimerUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Setumei();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
            SceneFlagManager.Instance.isPlayerMoving = !optionPanel.activeSelf;
        }
    }

    public void SaveButton()
    {
        savePanel.SetActive(!savePanel.activeSelf);
    }

    public void OptionButton(int timeScale)
    {
        Time.timeScale = timeScale;
        titlePanel.SetActive(!titlePanel.activeSelf);
        optionPanel.SetActive(!optionPanel.activeSelf);
    }

    void End1Mode()
    {
        Debug.Log(gameMode);
    }

    void End2Mode()
    {
        Debug.Log(gameMode);
    }

    void ResultMode()
    {
        Debug.Log(gameMode);
    }

    // リトライボタンが押された時の処理
    public void OnClickRetry()
    {
        SceneManager.LoadScene("SolicitationScene");
    }
}

[System.Serializable]
public class Sun
{
    [Header("何日目か")]
    public bool isAGE; // 日ごとのisAGE情報

    // 各日に異なる部屋に変化を起こすフラグのリスト
    [Header("部屋に変化を起こすフラグ")]
    public List<RoomFlag> roomFlags = new List<RoomFlag>();
}

[System.Serializable]
public class RoomFlag
{
    [Header("部屋の異変を起こすフラグ")]
    public bool isActive;
}