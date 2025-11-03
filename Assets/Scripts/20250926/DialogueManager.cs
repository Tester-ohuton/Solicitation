using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<DialogSentence> dialogSentences; // 全会話データ

    public Text dialogueText;
    public Text characterNameText;
    public GameObject dialogPanel;
    
    public int currentSituationID = 0;
    private int sentenceIndex = 0;

    void Start()
    {
        if (dialogueText == null || characterNameText == null)
        {
            Debug.LogError("DialogueManager: UI要素が割り当てられていません");
            return;
        }

        // 最初の会話をセット      // 必要に応じて状況IDによる分岐
        SetCurrentDialogue(currentSituationID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNextButtonClicked();
        }
    }

    public void SetCurrentDialogue(int id)
    {
        // IDで探して表示（同じIDが複数の場合は条件で分岐）
        DialogSentence ds = dialogSentences.Find(x => x.situationID == id);
        sentenceIndex = 0;
        ShowNextSentence(ds);
    }

    public void ShowNextSentence(DialogSentence ds)
    {
        if (ds == null)
        {
            dialogPanel.SetActive(false);
            //Debug.LogError("DialogSentenceが見つかりません (situationID: " + currentSituationID + ")");
            return;
        }
        
        if (ds.sentences == null)
        {
            dialogPanel.SetActive(false);
            //Debug.LogError("DialogSentence.sentencesがnullです (situationID: " + ds.situationID + ")");
            return;
        }

        if (sentenceIndex < ds.sentences.Count)
        {
            dialogueText.text = ds.sentences[sentenceIndex];
            characterNameText.text = ds.characterName;
            sentenceIndex++;
        }
        else
        {
            dialogueText.text = "";
            characterNameText.text = "";
            Debug.Log("会話終了 (situationID: " + ds.situationID + ")");

            // 次の会話が存在する場合は自動でセット
            currentSituationID++;
            DialogSentence nextDs = dialogSentences.Find(x => x.situationID == currentSituationID);
            if (nextDs != null)
            {
                SetCurrentDialogue(currentSituationID);
            }
        }
    }

    private void OnNextButtonClicked()
    {
        // ボタンイベントから呼び出し
        DialogSentence ds = dialogSentences.Find(x => x.situationID == currentSituationID);
        ShowNextSentence(ds);
    }
}

[System.Serializable]
public class DialogSentence
{
    public int situationID;                  // unique identifier for the dialog sentence
    public string characterName;    // character's name
    public List<string> sentences;  // list of sentences for the character

    public DialogSentence(int id, string name, List<string> sent)
    {
        situationID = id; // Default value, can be set later
        characterName = name;
        sentences = sent;
    }
}
