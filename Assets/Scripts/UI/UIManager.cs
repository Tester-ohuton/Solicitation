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
        eventButtonPanel.gameObject.SetActive(false); // ������Ԃł͔�\��
        eventEffectPanel.gameObject.SetActive(false); // ������Ԃł͔�\��
    }

    public void UpdateCardboardCount(int openedCount, int totalCount)
    {
        cardboardCountText.text = $"{openedCount}/{totalCount}";

        if (openedCount >= totalCount)
        {
            Debug.Log("�i�{�[���͂��ׂĊJ����!");

            // ���t���ƂɃR���v���[�g

        }
        else
        {
            //Debug.Log("�܂��J���Ă��Ȃ��i�{�[��������");
        }
    }

    public void UpdateDayUI(int currentDay)
    {
        dayText.text = $"{currentDay}����";
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
        eventButtonPanel.gameObject.SetActive(false); // �C�x���g������ɔ�\��
        eventEffectPanel.gameObject.SetActive(false);

        PlayerController.instance.isPlayerMoving = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
