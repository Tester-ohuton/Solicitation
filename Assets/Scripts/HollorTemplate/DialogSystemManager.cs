using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogSystemManager : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea; // インスペクターで機能の説明用

    [Header("有効かどうか")]
    public bool isActive;

    public TextMeshProUGUI nameText;

    [TextArea(1, 10)]
    public TextMeshProUGUI dialogText;

    private Queue<string> sentences;
    private string currentName;
    
    private GameObject player;
    private PlayerController playerController;

    void Start()
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

        if (!isActive)
            return;

        sentences = new Queue<string>();
    }

    public void StartDialog(string name, List<string> dialogSentences)
    {
        if (!isActive)
            return;

        currentName = name;
        nameText.text = currentName;
        sentences.Clear();

        foreach (string sentence in dialogSentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!isActive)
            return;

        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (!isActive)
            yield break;

        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        if (!isActive)
            return;

        // 会話終了時の処理
        SolicitationDirector.instance.Dialog(false);

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
