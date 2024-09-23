using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [TextArea(1, 10)]
    public string textArea;

    public AnomalyManager anomalyManager;

    // インスペクターで指定するための変数
    public int startRange1;
    public int endRange1;
    public int startRange2;
    public int endRange2;
    public int startRange3;
    public int endRange3;
    public int startRange4;
    public int endRange4;

    void Start()
    {
    }

    public void StartGame()
    {
        anomalyManager.ResetAnomalies();
        // ゲーム開始時の初期設定

        for (int i = startRange1; i < endRange1; i++) // インスペクターで指定した範囲
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date2Game()
    {
        for (int i = startRange2; i < endRange2; i++) // インスペクターで指定した範囲
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date3Game()
    {
        for (int i = startRange3; i < endRange3; i++) // インスペクターで指定した範囲
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date4Game()
    {
        for (int i = startRange4; i < endRange4; i++) // インスペクターで指定した範囲
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Retry()
    {
        StartGame();
        // リトライ時の処理

        // PlayerPrefsに保存されたすべてのデータを削除
        PlayerPrefs.DeleteAll();
    }
}
