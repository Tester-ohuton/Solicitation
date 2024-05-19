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

    // CountDownTimerPanel
    [Header("CountDownTimerPanel")] [SerializeField] GameObject countDownTimerPanel;

    [Header("CountDownTimer")]
    [SerializeField] CountDownTimer countDownTimer;

    // ItemTextPanel
    [Header("ItemTextPanel")] [SerializeField] GameObject itemTextPanel;

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

    void Start()
    {
        Time.timeScale = 1;

        titlePanel.SetActive(true);

        itemTextPanel.SetActive(false);
        setumeiPanel.SetActive(false);
        resultPanel.SetActive(false);
        savePanel.SetActive(false);
        optionPanel.SetActive(false);
        quitPanel.SetActive(false);
        countDownTimerPanel.SetActive(false);

        SceneFlagManager.Instance.isPlayerMoving = false;

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

    public void Setumei()
    {
        setumeiPanel.SetActive(!setumeiPanel.activeSelf);
    }

    public void Option()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);

        if (optionPanel.activeInHierarchy)
        {
            SceneFlagManager.Instance.isPlayerMoving = false;
        }
        else
        {
            SceneFlagManager.Instance.isPlayerMoving = true;
        }
    }

    void PrologueMode()
    {
        gameMode = GameMode.Title;
    }

    void TitleMode()
    {
        //Debug.Log(gameMode);
    }

    public void TitleButton()
    {
        titlePanel.SetActive(false);

        itemTextPanel.SetActive(true);
        SceneFlagManager.Instance.isPlayerMoving = true;
        setumeiPanel.SetActive(true);
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
        if (ItemText.instance.isComp)  // 段ボールを全て開けたら
        {
            countDownTimerPanel.SetActive(true);
            countDownTimer.TimerUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Setumei();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Option();
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