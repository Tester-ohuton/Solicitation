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

    public Vector3 initialPlayerPosition;
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
    public int totalDays = 4;
    public List<int> completedDays = new List<int>() { 1, 2, 3, 4, 5, 6 };

    public Dictionary<int, int> dayCardboardRequirements = new Dictionary<int, int>()
    {
        { 1, 6 },
        { 2, 2 },
        { 3, 2 },
        { 4, 3 }
    };

    private GameObject player;

    private GameObject game;
    private GameDirector gameDirector;

    private string saveFilePath;

    private void Start()
    {
        player = GameObject.Find("ChaM01_Player");

        if (player == null)
        {
            Debug.LogWarning("プレイヤーが見つかりません");
        }

        cardboardStates = new bool[6]; // フラグ配列の初期化（6つのCardboard用）
        SetSaveFilePath();


        game = GameObject.Find("GameDirector");

        if (game != null)
        {
            gameDirector = game.GetComponent<GameDirector>();
        }

        if (gameDirector != null)
        {
            gameDirector.Date1Game();
        }
        else
        {
            Debug.LogWarning("gameDirector is null");
        }

        StartNewGame();
    }

    private void SetSaveFilePath()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //webgl形式の場合は、保存先を固定パスにする
            saveFilePath = "savefile.json";
        }
        else
        {
            //その他の形式の場合は、保存先をpersistentDataPathにする
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
    }

    public void StartNewGame()
    {
        playerPosition = initialPlayerPosition;
        cardboardStates = new bool[cardboardStates.Length]; // フラグ配列の初期化
        volume = 1.0f;
        brightness = 1.0f;
        mouseSensitivity = 1f;
        taskProgress = 0;
        gameTime = TimeSpan.Zero;
        currentDay = 1;
        isCleared = false;
        isGameOver = false;
        isCursor = true;

        DeleteSaveData();
        SaveGame();
        LoadGame();
        ResetPlayerPos();
    }
    
    // Delete the save file if it exists
    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        else
        {
            Debug.LogWarning("ファイルパスが見つかりません: " + saveFilePath);
        }
    }

    public void Retry()
    {
        string thisSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisSceneName);
    }

    public void ResetPlayerPos()
    {
        player.transform.position = playerPosition;
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
            Debug.Log("セーブファイルを書き込みます: " + saveFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("セーブファイルはエラーで書き込むことができません: " + e.Message);
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

                Debug.Log("ファイルの場所: " + saveFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("何らかのメッセージを取得しました: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("ファイルパスは警告が出てわかりません: " + saveFilePath);
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
                return false;
            }
        }
        Debug.Log("All cardboards have been interacted with.");
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
