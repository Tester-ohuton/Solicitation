using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    [TextArea]
    public string text;

    public Text cardboardCountText;
    public Text dayText;
    public Text gameTimeText;

    public Button eventButton;
    public GameObject eventButtonPanel;

    private int openedCardboardCount = 0;
    private int currentCardboardCount = 0;

    void Start()
    {
        if (eventButton != null)
            eventButton.onClick.AddListener(OnEventButtonClicked);

        if (eventButtonPanel != null)
            eventButtonPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance != null)
            UpdateGameTimeUI(GameManager.instance.GetFormattedGameTime());

        if (eventButtonPanel != null && eventButtonPanel.activeInHierarchy)
        {
            if (PlayerController.instance != null)
                PlayerController.instance.isPlayerMoving = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            UpdateCardboardCount(openedCardboardCount, currentCardboardCount);
        }
        else
        {
            if (PlayerController.instance != null)
                PlayerController.instance.isPlayerMoving = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UpdateCardboardCount(int openedCount, int totalCount)
    {
        if (cardboardCountText != null)
            cardboardCountText.text = $"{openedCount}/{totalCount}";

        if (openedCount >= totalCount && totalCount > 0)
        {
            Debug.Log("段ボールはすべて開けた!");

            int currentDay = GameManager.instance.currentDay;
            if (!GameManager.instance.completedDays.Contains(currentDay))
            {
                GameManager.instance.completedDays.Add(currentDay);
                UpdateDayUI(currentDay);
            }
        }
        else
        {
            if (GameManager.instance != null)
                UpdateGameTimeUI(GameManager.instance.GetFormattedGameTime());
        }
    }

    public void UpdateDayUI(int currentDay)
    {
        if (dayText != null)
            dayText.text = $"Day:{currentDay}";
    }

    // 日付からその日の総段ボール数を読み込み、開けた数をリセットして UI 更新
    public void UpdateCardboardCount(int currentDay)
    {
        if (GameManager.instance != null)
        {
            currentCardboardCount = GameManager.instance.dayCardboardRequirements.ContainsKey(currentDay)
                ? GameManager.instance.dayCardboardRequirements[currentDay]
                : 0;
        }
        else
        {
            currentCardboardCount = 0;
        }

        openedCardboardCount = 0;
        UpdateCardboardCount(openedCardboardCount, currentCardboardCount);
    }

    // --- 追加: 開けた段ボール数を増やす / 設定する API ---
    public void IncrementOpenedCardboard()
    {
        openedCardboardCount = Mathf.Clamp(openedCardboardCount + 1, 0, currentCardboardCount);
        UpdateCardboardCount(openedCardboardCount, currentCardboardCount);
    }

    public void SetOpenedCardboardCount(int count)
    {
        openedCardboardCount = Mathf.Clamp(count, 0, currentCardboardCount);
        UpdateCardboardCount(openedCardboardCount, currentCardboardCount);
    }

    public void ResetOpenedCardboardCount()
    {
        openedCardboardCount = 0;
        UpdateCardboardCount(openedCardboardCount, currentCardboardCount);
    }
    // -------------------------------------------------------

    public void UpdateGameTimeUI(string formattedTime)
    {
        if (gameTimeText != null)
            gameTimeText.text = formattedTime;
    }

    public void ShowEventButton()
    {
        if (eventButtonPanel != null)
            eventButtonPanel.SetActive(true);

        if (eventButtonPanel != null && eventButtonPanel.activeInHierarchy)
        {
            if (PlayerController.instance != null)
                PlayerController.instance.isPlayerMoving = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnEventButtonClicked()
    {
        EventManager.Instance.CompleteCurrentEvent();

        if (eventButtonPanel != null)
            eventButtonPanel.SetActive(false);

        if (PlayerController.instance != null)
            PlayerController.instance.isPlayerMoving = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
