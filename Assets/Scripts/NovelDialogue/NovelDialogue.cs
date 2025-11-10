using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // 新InputSystem対応

public class NovelDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)]
        public string text; // セリフ内容
        public string speaker; // 話者名（空なら主人公）
    }

    [Header("会話リスト")]
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

    [Header("UI参照")]
    public Text speakerText;
    public Text dialogueText;
    public CanvasGroup dialogueCanvas;

    [Header("表示設定")]
    public float textSpeed = 0.02f;

    private int currentIndex = 0;
    private bool isTyping = false;
    private bool canProceed = true;

    void Start()
    {
        dialogueCanvas.alpha = 1;
        ShowLine();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed || !canProceed) return;

        if (isTyping)
        {
            // スキップして全文表示
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentIndex].text;
            isTyping = false;
        }
        else
        {
            NextLine();
        }
    }

    void ShowLine()
    {
        if (currentIndex >= dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines[currentIndex];
        speakerText.text = line.speaker == "" ? "主人公" : line.speaker;
        StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        currentIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        dialogueCanvas.alpha = 0;
        canProceed = false;
        Debug.Log("会話終了");
    }
}
