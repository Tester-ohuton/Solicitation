using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DialogLists : MonoBehaviour
{
    [TextArea]
    public string text;

    public DialogSystemManager dialogManager;

    public string titleName = "Game Introduction";
    public List<string> titleDialogSentences;

    public string cardboardInteractionName = "Player";
    public List<string> cardboardDialogSentences;

    public string entranceInteractionName = "Player";
    public List<string> entranceDialogSentences;


    /*public Button dialogButton;*/


    public string interactTag = "Cardboard";
    public string entranceTag = "Entrance";
    public float interactRange = 3f;
    public LayerMask interactableLayer;

    private bool isTitleDialogStarted = false;  // ダイアログがすでに開始されたかどうかを追跡
    private bool isCardboardDialog = false;
    private bool isEntranceDialog = false;

    void Start()
    {
        if (!isTitleDialogStarted) // すでに開始されたかどうかを確認
        {
            if (dialogManager != null && titleDialogSentences.Count > 0)
            {
                dialogManager.StartDialog(titleName, titleDialogSentences);
                isTitleDialogStarted = true;
            }
            else
            {
                Debug.Log("No title dialog available.");
            }
        }
        else
        {
            dialogManager.DisplayNextSentence(); // 2回目以降は次の文章を表示
        }

        /*
        dialogButton.onClick.AddListener(() =>
        {
            dialogManager.DisplayNextSentence(); // 2回目以降は次の文章を表示
        });
        */
    }

    private void Update()
    {
        CheckForRayHitCardboard();

        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Return))
        {
            dialogManager.DisplayNextSentence(); // 2回目以降は次の文章を表示
        }
    }

    void CheckForRayHitCardboard()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            if (hit.collider.CompareTag(interactTag))
            {
                if (!isCardboardDialog)
                {
                    SolicitationDirector.instance.Dialog(true);
                    HandleCardboardInteraction();
                }
            }
            else if (hit.collider.CompareTag(entranceTag))
            {
                if (!isEntranceDialog)
                {
                    SolicitationDirector.instance.Dialog(true);
                    HandleEntranceInteraction();
                }
            }
        }
        else
        {
            return;
        }
    }

    void HandleCardboardInteraction()
    {
        if (dialogManager != null && cardboardDialogSentences.Count > 0)
        {
            dialogManager.StartDialog(cardboardInteractionName, cardboardDialogSentences);
            isCardboardDialog = true;
        }
        else
        {
            Debug.Log("No Cardboard dialog available.");
        }
    }

    void HandleEntranceInteraction()
    {
        if (dialogManager != null && entranceDialogSentences.Count > 0)
        {
            dialogManager.StartDialog(entranceInteractionName, entranceDialogSentences);
            isEntranceDialog = true;
        }
        else
        {
            Debug.Log("No Entrance dialog available.");
        }
    }
}
