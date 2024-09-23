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

    public Vector3 initialPlayerPosition; // �v���C���[�̏����ʒu
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
        // �Q�[���I�u�W�F�N�g�̎擾
        player = GameObject.FindGameObjectWithTag("Player");
        resetPos = GameObject.FindGameObjectWithTag("GameController");

        if (player == null)
        {
            Debug.LogWarning("�I�u�W�F�N�g���F 'Player'��������܂���");
        }

        if (resetPos == null)
        {
            Debug.LogWarning("�I�u�W�F�N�g���F 'resetPos'��������܂���");
        }

        // �v���b�g�t�H�[���ɉ����ăt�@�C���p�X��ݒ�
        SetSaveFilePath();


        game = GameObject.Find("GameDirector");

        if (game != null)
        {
            gameDirector = game.GetComponent<GameDirector>();
        }

        if (gameDirector != null)
        {
            gameDirector.StartGame(); // �i�{�[�������z�u
            Debug.Log("����ɒi�{�[�����z�u����܂���");
        }
        else
        {
            //Debug.Log("gameDirector��������܂���");
        }

    }

    private void SetSaveFilePath()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // WebGL�ł�IndexedDB�ɕۑ�����邽�߁A�t�@�C���p�X�͑��΃p�X
            saveFilePath = "savefile.json";
        }
        else
        {
            // PC�v���b�g�t�H�[���ł�`Application.persistentDataPath`���g�p
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
    }

    // �V�����Q�[�����J�n����ۂ̏���������
    public void StartNewGame()
    {
        playerPosition = initialPlayerPosition;
        cardboardStates = new bool[cardboardStates.Length]; // ���݂�Day��Cardboard�̏�Ԃ�������
        volume = 1.0f;
        brightness = 1.0f;
        mouseSensitivity = 1f;
        taskProgress = 0;
        gameTime = TimeSpan.Zero;
        currentDay = 1;
        isCleared = false;
        isGameOver = false;

        // �Z�[�u�f�[�^���폜�i�K�v�ɉ����āj
        DeleteSaveData();

        // �K�v�Ȃ�V�[�������Z�b�g���ăQ�[�����J�n
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �Z�[�u�f�[�^�̍폜����
    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("�Z�[�u�f�[�^���폜���܂���: " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("�Z�[�u�f�[�^�����݂��܂���: " + saveFilePath);
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
            Debug.Log("�Q�[���f�[�^��ۑ����܂���: " + saveFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("�Q�[���f�[�^�̕ۑ��Ɏ��s���܂���: " + e.Message);
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

                Debug.Log("�Q�[���f�[�^��ǂݍ��݂܂���: " + saveFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("�Q�[���f�[�^�̓ǂݍ��݂Ɏ��s���܂���: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("�ۑ��f�[�^�����݂��܂���: " + saveFilePath);
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
                Debug.Log("�܂��C���^���N�g����Ă��Ȃ�Cardboard������");
                return false;
            }
        }
        Debug.Log("���ׂĂ�Cardboard���C���^���N�g���ꂽ");
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
