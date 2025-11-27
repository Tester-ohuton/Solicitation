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

    [Header("Playerがいる場合カメラはオフ")]
    [SerializeField] GameObject cameraObject;

    public KeyCode pressKey_1 = KeyCode.Escape;
    
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

            if(optionPanel)
            {
                Time.timeScale = 0f;
                cameraObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                cameraObject.SetActive(false);
            }
        }
    }
}