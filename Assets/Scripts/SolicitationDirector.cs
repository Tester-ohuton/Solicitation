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
    }

    [Header("OptionPanel")]
    [SerializeField] GameObject optionPanel;

    public KeyCode pressKey_1 = KeyCode.Tab;

    [Header("DialogPanel")]
    [SerializeField] GameObject dialogPanel;

    public KeyCode pressKey_2 = KeyCode.D;

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
            Debug.Log("プレイヤーコントローラーが見つかりません");
        }

        Time.timeScale = 1;

        StartGame();
    }

    // Method to update the game state
    public void UpdateGame()
    {
        if (Input.GetKeyDown(pressKey_1))
        {
            OptionKeyPress(); // Option Window true/false
        }

        if (Input.GetKeyDown(pressKey_2))
        {
            Dialog(); // DialogWindow Window true/false
        }
    }

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
    /// 会話パートで時間が止まる,DialogWindow表示
    /// 探索から始まる,必要に応じてDialogWindow非表示
    /// </summary>
    public void Dialog()
    {
        if (playerController == null)
        {
            Debug.Log("Playerが見つかりません");
        }
        else
        {
            playerController.isPlayerMoving = false;
        }

        dialogPanel.SetActive(!dialogPanel.activeSelf);

        if (!dialogPanel.activeInHierarchy)
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
        if (playerController == null)
        {
            Debug.Log("Playerが見つかりません"); return;
        }
        else
        {
            if (optionPanel.activeInHierarchy || dialogPanel.activeInHierarchy)
            {
                playerController.isPlayerMoving = false;
            }
            else
            {
                Debug.LogWarning("Windowは表示されていない");
            }
        }

        GameManager.instance.isGameOver = false;
    }
}