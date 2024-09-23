using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [TextArea]
    public string text;

    public Vector3 initialPlayerPosition; // プレイヤーの初期位置
    public Vector3 playerPosition;
    public bool[] cardboardStates;
    public float volume;
    public float brightness;
    public float mouseSensitivity;
    public int taskProgress;
    private TimeSpan gameTime;
    public int currentDay = 1;
    public bool isCleared = false;
    public bool isGameOver = false;
    public bool isCursor = true;

    public Dictionary<int, int> dayCardboardRequirements = new Dictionary<int, int>()
    {
        { 1, 6 },
        { 2, 2 },
        { 3, 2 },
        { 4, 2 }
    };

    private GameObject player;
    private GameObject resetPos;

    private GameObject game;
    private GameDirector gameDirector;

    private string saveFilePath;

    private void Start()
    {
        // ゲームオブジェクトの取得
        player = GameObject.FindGameObjectWithTag("Player");
        resetPos = GameObject.FindGameObjectWithTag("GameController");

        if (player == null)
        {
            Debug.LogWarning("オブジェクト名： 'Player'が見つかりません");
        }

        if (resetPos == null)
        {
            Debug.LogWarning("オブジェクト名： 'resetPos'が見つかりません");
        }

        // プラットフォームに応じてファイルパスを設定
        SetSaveFilePath();


        game = GameObject.Find("GameDirector");

        if (game != null)
        {
            gameDirector = game.GetComponent<GameDirector>();
        }

        if (gameDirector != null)
        {
            gameDirector.StartGame(); // 段ボール初期配置
            Debug.Log("正常に段ボールが配置されました");
        }
        else
        {
            //Debug.Log("gameDirectorが見つかりません");
        }

    }

    private void SetSaveFilePath()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // WebGLではIndexedDBに保存されるため、ファイルパスは相対パス
            saveFilePath = "savefile.json";
        }
        else
        {
            // PCプラットフォームでは`Application.persistentDataPath`を使用
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
    }

    // 新しいゲームを開始する際の初期化処理
    public void StartNewGame()
    {
        playerPosition = initialPlayerPosition;
        cardboardStates = new bool[cardboardStates.Length]; // 現在のDayのCardboardの状態を初期化
        volume = 1.0f;
        brightness = 1.0f;
        mouseSensitivity = 1f;
        taskProgress = 0;
        gameTime = TimeSpan.Zero;
        currentDay = 1;
        isCleared = false;
        isGameOver = false;

        // セーブデータを削除（必要に応じて）
        DeleteSaveData();

        // 必要ならシーンをリセットしてゲームを開始
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // セーブデータの削除処理
    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("セーブデータを削除しました: " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("セーブデータが存在しません: " + saveFilePath);
        }
    }

    public void Retry()
    {
        string thisSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisSceneName);
    }

    public void ResetPlayerPos()
    {
        player.transform.position = resetPos.transform.position;
    }

    public int Day()
    {
        return currentDay;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            cardboardStates = cardboardStates,
            volume = volume,
            brightness = brightness,
            mouseSensitivity = mouseSensitivity,
            taskProgress = taskProgress,
            gameTime = gameTime.ToString(@"hh\:mm\:ss"),
            currentDay = currentDay,
            isCleared = isCleared,
            isGameOver = isGameOver
        };

        string json = JsonUtility.ToJson(saveData);

        try
        {
            File.WriteAllText(saveFilePath, json);
            Debug.Log("ゲームデータを保存しました: " + saveFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("ゲームデータの保存に失敗しました: " + e.Message);
        }
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                playerPosition = saveData.playerPosition;
                cardboardStates = saveData.cardboardStates;
                volume = saveData.volume;
                brightness = saveData.brightness;
                mouseSensitivity = saveData.mouseSensitivity;
                taskProgress = saveData.taskProgress;
                gameTime = TimeSpan.Parse(saveData.gameTime);
                currentDay = saveData.currentDay;
                isCleared = saveData.isCleared;
                isGameOver = saveData.isGameOver;

                player.transform.position = playerPosition;

                Debug.Log("ゲームデータを読み込みました: " + saveFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("ゲームデータの読み込みに失敗しました: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("保存データが存在しません: " + saveFilePath);
        }
    }

    public void UpdateGameTime(float deltaTime)
    {
        gameTime += TimeSpan.FromSeconds(deltaTime);
    }

    public string GetFormattedGameTime()
    {
        return gameTime.ToString(@"hh\:mm\:ss");
    }

    public bool AreAllCardboardsInteracted()
    {
        foreach (bool state in cardboardStates)
        {
            if (!state)
            {
                Debug.Log("まだインタラクトされていないCardboardがある");
                return false;
            }
        }
        Debug.Log("すべてのCardboardがインタラクトされた");
        return true;
    }

    [System.Serializable]
    private class SaveData
    {
        public Vector3 playerPosition;
        public bool[] cardboardStates;
        public float volume;
        public float brightness;
        public float mouseSensitivity;
        public int taskProgress;
        public string gameTime;
        public int currentDay;
        public bool isCleared;
        public bool isGameOver;
    }
}
