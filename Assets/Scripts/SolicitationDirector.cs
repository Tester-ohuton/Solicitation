using System;
using System.Collections;
using UnityEngine;

/*
 各シーンのC#スクリプトを管理
 Panelを管理
 */
public class SolicitationDirector : MonoBehaviour
{
    public static SolicitationDirector instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [Header("OptionPanel")]
    [SerializeField] GameObject optionPanel;

    public KeyCode pressKey = KeyCode.Tab;
    
    [Header("DialogPanel")]
    [SerializeField] GameObject dialogPanel;

    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        player = GameObject.Find("ChaM01_Player");

        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("プレイヤーが見つかりません");
        }

        Time.timeScale = 1;

        //StartCoroutine(SetDirection());
        SetGame();
    }

    private void Update()
    {
        UpdateGame();
    }

    // Method to update the game state
    private void UpdateGame()
    {
        if (Input.GetKeyDown(pressKey))
        {
            OptionKeyPress(); // Option Window true/false
        }
    }

    public void OptionKeyPress() /* ボタンからも呼ぶ */
    {
        optionPanel.SetActive(!optionPanel.activeSelf);

        if (!optionPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (playerController != null)
            {
                playerController.isPlayerMoving = true;
            }
            else
            {
                //Debug.Log("Playerが見つかりません");
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

            if (playerController != null)
            {
                playerController.isPlayerMoving = false;
            }
            else
            {
                //Debug.Log("Playerが見つかりません");
            }
        }
    }

    public void Dialog(bool isActive)
    {
        dialogPanel.SetActive(isActive);

        if(isActive)
        {
            Cursor.lockState = CursorLockMode.None;

            if (playerController != null)
            {
                playerController.isPlayerMoving = false;
            }
            else
            {
                //Debug.Log("Playerが見つかりません");
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (playerController != null)
            {
                playerController.isPlayerMoving = true;
            }
            else
            {
                //Debug.Log("Playerが見つかりません");
            }
        }
    }

    /*ボタンから呼ぶ*/
    public void OnQuit()
    {
        Application.Quit();
    }

    /*
    private IEnumerator SetDirection()
    {
        isLoading = true;

        yield return new WaitForSeconds(3f);

        isLoading = false;
    }
    */

    /*ボタンから呼ぶ*/
    public void SetGame()
    {
        // Enable dialog mode through your director system
        Dialog(true);

        if (playerController != null)
        {
            if (optionPanel.activeInHierarchy || dialogPanel.activeInHierarchy)
            {
                playerController.isPlayerMoving = false;
            }
            else
            {
                playerController.isPlayerMoving = true;
            }
        }
        else
        {
            //Debug.Log("Playerが見つかりません");
        }

        GameDirector.instance.Retry();

        // ゲーム開始時にカーソルを中央に配置し、表示する
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}