using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolicitationDirector : MonoBehaviour
{
    public static SolicitationDirector instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("OptionPanel")]
    [SerializeField] GameObject optionPanel;

    public KeyCode pressKey_1 = KeyCode.Tab;

    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        Time.timeScale = 1;

        StartGame();
    }

    private void Update()
    {
        StartGame();
        UpdateGame();
    }

    // Method to update the game state
    private void UpdateGame()
    {
        if (Input.GetKeyDown(pressKey_1))
        {
            OptionKeyPress(); // Option Window true/false
        }
    }

    /// <summary>
    /// まとめてオプションパネルを開く/閉じる
    /// </summary>
    public void OptionKeyPress()
    {
        if (playerController == null)
        {
            Debug.Log("Playerが見つかりません");
        }
        else
        {
            playerController.isPlayerMoving = false;
        }
        
        optionPanel.SetActive(!optionPanel.activeSelf);

        if (!optionPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    /// <summary>
    /// プレイヤーが探索したり動くことができる時間
    /// </summary>
    public void StartGame()
    {
        player = GameObject.Find("ChaM01_Player");

        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();

            if (optionPanel.activeInHierarchy)
            {
                playerController.isPlayerMoving = false;
            }
            else
            {
                playerController.isPlayerMoving = true;
                Debug.LogWarning("PauseWindowは表示されていない");
            }
        }
        else
        {
            Debug.Log("プレイヤーコントローラーが見つかりません");
        }

        GameManager.instance.isGameOver = false;
    }
}