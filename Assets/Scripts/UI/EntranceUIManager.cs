using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class EntranceUIManager : MonoBehaviour
{
    public static EntranceUIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static UnityEvent OnEntranceUI = new UnityEvent();

    [TextArea]
    public string text;

    public GameObject entranceUIPanel; // Panel that contains all the buttons
    public GameObject rockUIPanel;
    public Button peekButton;
    public Button openDoorButton;
    public Button cancelButton;
    public Button rockButton;

    public Camera mainCamera;
    public Camera peekingCamera;

    public bool showPeekButton = true;
    public bool showOpenDoorButton = false;
    public bool showCancelButton = true;
    public bool showRockButton = false;

    [Space]
    [SerializeField] private DoorController doorController;

    [Space]
    [SerializeField] private TriggerFlag triggerFlag;

    public PlayerController playerController;

    void Start()
    {
        // Initialize the UI visibility based on Inspector settings
        UpdateButtonVisibility();

        mainCamera.gameObject.SetActive(true);
        peekingCamera.gameObject.SetActive(false);

        peekButton.onClick.AddListener(() =>
        {
            HideUI();
            StartCoroutine(PeekThroughDoor());
        });

        openDoorButton.onClick.AddListener(() =>
        {
            HideUI();
            OpenDoor();
        });

        cancelButton.onClick.AddListener(() =>
        {
            HideUI();
        });

        rockButton.onClick.AddListener(() =>
        {
            RockUIPanelActive(true);
        });

        OnEntranceUI.AddListener(() =>
        {
            ActiveUI();
        });
    }

    private void Update()
    {
        Answer(true); // 7日目に表示
    }

    public void RockUIPanelActive(bool isActive)
    {
        rockUIPanel.SetActive(isActive);
    }

    public void UpdateButtonVisibility()
    {
        // Show or hide buttons based on the Inspector flags
        peekButton.gameObject.SetActive(showPeekButton);
        openDoorButton.gameObject.SetActive(showOpenDoorButton);
        cancelButton.gameObject.SetActive(showCancelButton);
        rockButton.gameObject.SetActive(showRockButton);
    }

    public bool Answer(bool isActive)
    {
        if (7 <= GameManager.instance.Day() && GameManager.instance.AreAllCardboardsInteracted()) // 7日目にすべて荷物を開けたら
        {
            peekButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);

            // 2択
            peekButton.gameObject.SetActive(isActive);
            openDoorButton.gameObject.SetActive(isActive);
            rockButton.gameObject.SetActive(isActive);
        }

        return isActive;
    }

    // Method to be called by the Peek button
    public IEnumerator PeekThroughDoor()
    {
        // Implement the logic for peeking through the door
        Debug.Log("Peeking through the door...");
        // Camera switching logic would go here
        mainCamera.gameObject.SetActive(false);
        peekingCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        mainCamera.gameObject.SetActive(true);
        peekingCamera.gameObject.SetActive(false);
    }

    // Method to be called by the Open Door button
    public void OpenDoor()
    {
        // Implement the logic for opening the door
        Debug.Log("Opening the entranceDoor...");
        // Door opening animation logic would go here
        if (doorController != null)
        {
            Debug.Log("Door が見つかりました。");
            doorController.OpenDoorAnimation();
        }
        else
        {
            Debug.LogWarning("Animator が見つかりませんでした。");
        }
    }

    // Method to be called by the Cancel button
    public void HideUI()
    {
        entranceUIPanel.SetActive(false);
    }

    public void ActiveUI()
    {
        entranceUIPanel.SetActive(true);
    }
}
