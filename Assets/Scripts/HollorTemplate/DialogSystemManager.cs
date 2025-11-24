using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogSystemManager : MonoBehaviour
{
    [TextArea(1, 10)]
    [Header("Dialog Sentences")]
    public string textArea;

    public Text nameText;
    public Text dialogText;

    private Queue<DialogSentence> dialogQueue;
    private int currentSentenceIndex;

    void Start()
    {
        // 修正箇所：Queue を正しく初期化
        dialogQueue = new Queue<DialogSentence>();
        currentSentenceIndex = 0;
    }

    void Update()
    {
        if (dialogQueue.Count > 0 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            ProceedToNextSentence();
        }
    }

    public void StartDialog(List<DialogSentence> sentences)
    {
        foreach (var sentence in sentences)
        {
            dialogQueue.Enqueue(sentence);
        }
        currentSentenceIndex = 0;
        DisplayFirstSentence();
    }

    private void DisplayFirstSentence()
    {
        if (dialogQueue.Count > 0)
        {
            var currentDialog = dialogQueue.Peek();
            if (currentSentenceIndex < currentDialog.sentences.Count)
            {
                string sentence = currentDialog.sentences[currentSentenceIndex];
                string character = currentDialog.characterName;
                nameText.text = character;
                dialogText.text = sentence;
            }
        }
    }

    private void ProceedToNextSentence()
    {
        if (dialogQueue.Count == 0)
        {
            return;
        }

        var currentDialog = dialogQueue.Peek();
        currentSentenceIndex++;
        
        if (currentSentenceIndex < currentDialog.sentences.Count)
        {
            DisplayNextSentence();
        }
        else
        {
            dialogQueue.Dequeue();

            currentSentenceIndex = 0;

            if (dialogQueue.Count > 0)
            {
                DisplayFirstSentence();
            }
            else
            {
                return;
            }
        }
    }

    private void DisplayNextSentence()
    {
        var currentDialog = dialogQueue.Peek();
        if (currentSentenceIndex < currentDialog.sentences.Count)
        {
            string sentence = currentDialog.sentences[currentSentenceIndex];
            string character = currentDialog.characterName;
            nameText.text = character;
            dialogText.text = sentence;
        }
    }
}
