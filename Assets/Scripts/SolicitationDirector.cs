using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SolicitationDirector : MonoBehaviour
{
    public float timer;

    // 時間
    [SerializeField] TextMeshProUGUI textGameTimer;

    // SetumeiPanel
    [SerializeField] GameObject setumeiPanel;

    // titlePanel
    [SerializeField] GameObject titlePanel;

    // ResultPanel
    [SerializeField] GameObject clearPanel;

    // SavePanel
    [SerializeField] GameObject savePanel;

    // OptionPanel
    [SerializeField] GameObject optionPanel;

    // QuitPanel
    [SerializeField] GameObject quitPanel;

    private float currentTimer;

    // ゲームモード
    enum GameMode
    {
        Prologue,
        Title,
        Game,
        Game1,
        Game2,
        Game3,
        Game4,
        Game5,
        Game6,
        End1,
        End2,
        Result,
    }

    GameMode gameMode;

    void Start()
    {
        Time.timeScale = 1;

        setumeiPanel.SetActive(false);
        titlePanel.SetActive(true);
        clearPanel.SetActive(false);
        savePanel.SetActive(false);
        optionPanel.SetActive(false);
        quitPanel.SetActive(false);

        currentTimer = timer;

        // 最初のモード
        gameMode = GameMode.Prologue;
    }

    void Update()
    {
        if (currentTimer <= 0)
        {
            ShowGameClearPanel();
            currentTimer = -1;
            return;
        }

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
            case GameMode.Game1:
                Game1Complete();
                break;
            case GameMode.Game2:
                Game2Complete();
                break;
            case GameMode.Game3:
                Game3Complete();
                break;
            case GameMode.Game4:
                Game4Complete();
                break;
            case GameMode.Game5:
                Game5Complete();
                break;
            case GameMode.Game6:
                Game6Complete();
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

    void Setumei()
    {
        setumeiPanel.SetActive(!setumeiPanel.activeSelf);
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
        titlePanel.SetActive(false);
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
        currentTimer -= Time.deltaTime;
        textGameTimer.text = currentTimer.ToString("F0");

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Setumei();
        }

        if (ItemText.instance.isOne)
        {
            gameMode = GameMode.Game1;
            //Debug.Log(gameMode);
        }
        if (ItemText.instance.isTwo)
        {
            gameMode = GameMode.Game2;
            //Debug.Log(gameMode);
        }
        if (ItemText.instance.isThree)
        {
            gameMode = GameMode.Game3;
            //Debug.Log(gameMode);
        }
        if (ItemText.instance.isFour)
        {
            gameMode = GameMode.Game4;
            //Debug.Log(gameMode);
        }
        if (ItemText.instance.isFive)
        {
            gameMode = GameMode.Game5;
            //Debug.Log(gameMode);
        }
        if (ItemText.instance.isComp)
        {
            gameMode = GameMode.Game6;
            //Debug.Log(gameMode);
        }
    }

    void Game1Complete()
    {
        gameMode = GameMode.Game;
    }

    void Game2Complete()
    {
        gameMode = GameMode.Game;
    }

    void Game3Complete()
    {
        gameMode = GameMode.Game;
    }

    void Game4Complete()
    {
        gameMode = GameMode.Game;
    }

    void Game5Complete()
    {
        gameMode = GameMode.Game;
    }

    void Game6Complete()
    {
        gameMode = GameMode.Game;
    }

    public void SaveButton()
    {
        titlePanel.SetActive(!titlePanel.activeSelf);
        savePanel.SetActive(!savePanel.activeSelf);
    }

    public void OptionButton(int timeScale)
    {
        SceneFlagManager.Instance.isPlayerMoving = false;
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

    private void ShowGameClearPanel()
    {
        clearPanel.SetActive(true);

        // Updateを停止
        enabled = false;
    }

    // リトライボタンが押された時の処理
    public void OnClickRetry()
    {
        SceneManager.LoadScene("SolicitationScene");
    }
}
