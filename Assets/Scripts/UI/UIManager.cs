using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [TextArea]
    public string text;

    public TextMeshProUGUI cardboardCountText;
    public TextMeshProUGUI dayText;  // Add this UI Text element for the day display
    public TextMeshProUGUI gameTimeText;  // Add this UI Text element for game time display

    public Button eventButton;
    public GameObject eventButtonPanel;
    public GameObject eventEffectPanel;

    void Start()
    {
        eventButton.onClick.AddListener(OnEventButtonClicked);
        eventButtonPanel.gameObject.SetActive(false); // 初期状態では非表示
        eventEffectPanel.gameObject.SetActive(false); // 初期状態では非表示
    }

    public void UpdateCardboardCount(int openedCount, int totalCount)
    {
        cardboardCountText.text = $"{openedCount}/{totalCount}";

        if (openedCount >= totalCount)
        {
            Debug.Log("段ボールはすべて開けた!");

            // 日付ごとにコンプリート

        }
        else
        {
            //Debug.Log("まだ開けていない段ボールがある");
        }
    }

    public void UpdateDayUI(int currentDay)
    {
        dayText.text = $"{currentDay}日目";
        UpdateCardboardCount(0, 
            GameManager.instance.dayCardboardRequirements.ContainsKey(currentDay)
            ? GameManager.instance.dayCardboardRequirements[currentDay]
            : 0);
    }

    public void UpdateGameTimeUI(string formattedTime)
    {
        gameTimeText.text = formattedTime;
    }

    public void ShowEventButton()
    {
        eventButtonPanel.gameObject.SetActive(true);
        eventEffectPanel.gameObject.SetActive(true);

        if(eventButtonPanel.activeInHierarchy)
        {
            PlayerController.instance.isPlayerMoving = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnEventButtonClicked()
    {
        EventManager.Instance.CompleteCurrentEvent();
        eventButtonPanel.gameObject.SetActive(false); // イベント完了後に非表示
        eventEffectPanel.gameObject.SetActive(false);

        PlayerController.instance.isPlayerMoving = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
