using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogLists : MonoBehaviour
{
    [TextArea(1, 10)]
    [Header("Dialog Sentences")]
    public string textArea; 

    public static DialogLists instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public DialogSystemManager dialogSystemManager;

    [Header("Dialog Sentences")]
    public List<DialogSentence> entranceDialogSentences; 

    public readonly string interactTag = "Cardboard";
    public readonly string entranceTag = "Entrance";
    private readonly string satoTag = "Sato";
    public float interactRange = 3f;
    public LayerMask interactableLayer;

    public float sollicitationTirasi;

    private bool isCardboardDialogStarted = false;
    private bool isEntranceDialogStarted = false;   // 入口
    private bool isSatoDialogStarted = false;
    

    private void Start()
    {
        dialogSystemManager.StartDialog(entranceDialogSentences);
    }

    private void Update()
    {
        if(GameManager.instance.isGameOver)
        {
            return;
        }

        if (dialogSystemManager == null)
        {
            Debug.Log("Dialog System Manager is missing.");
            return;
        }

        if (!isEntranceDialogStarted)
        {
            CheckForRayHitCardboard();
        }
        else
        {
            ItemOpenedCondition();
        }
    }

    void ItemOpenedCondition()
    {
        var ItemLogger = GameObject.FindGameObjectsWithTag("Cardboard");

        Item itemScript = null;

        // 2025/10/01 必要機能の付け足し
        isCardboardDialogStarted = false;

        while(!isCardboardDialogStarted)
        {
            for (int i = 0; i < ItemLogger.Length; i++)
            {
                if (ItemLogger.Length > 0)
                {
                    itemScript = ItemLogger[i].GetComponent<Item>();
                    itemScript.isOK = true;

                    if (itemScript.isOK)
                    {
                        SolicitationDirector.instance.Dialog();
                        isCardboardDialogStarted = true;
                    }
                    else
                    {
                        Debug.Log("Itemが見つかりません");
                    }

                    break; // 一度だけ実行するためにループを抜ける
                }
                else
                {
                    if (ItemLogger.Length == 0)
                    {
                        Debug.Log("すべて回収してクリア！");
                    }
                }
            }
        }
    }

    void CheckForRayHitCardboard()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            if (hit.collider.CompareTag(satoTag) && !isSatoDialogStarted)
            {
                isSatoDialogStarted = true;
            }
            else
            {
                SolicitationDirector.instance.Dialog();
            }
        }
    }
}