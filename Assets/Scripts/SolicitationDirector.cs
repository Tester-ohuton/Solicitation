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
    public PlayerController playerController;
    
    private void Start()
    {
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
            // Debug.Log("PauseWindowが表示されている");
            Time.timeScale = 0;
            // カーソルを表示
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            // カーソルを非表示
            Cursor.visible = false;
            //Debug.LogWarning("PauseWindowは表示されていない");
        }
    }
}