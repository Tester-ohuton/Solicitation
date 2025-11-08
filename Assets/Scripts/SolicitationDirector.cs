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
        UpdateGame();
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
        }
        else
        {
            Debug.Log("プレイヤーコントローラーが見つかりません");
        }

        GameManager.instance.isGameOver = false;
        GameManager.instance.isCleared = false;
    }


    /// <summary>
    /// プレイヤーを動かす
    /// </summary>
    private void UpdateGame()
    {
        if (Input.GetKeyDown(pressKey_1))
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }

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
}