using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string npcName;
    public string[] dialogue;
}

public class DialogueLoader : MonoBehaviour
{
    public string jsonFileName;
    public Dialogue dialogueData;
    public Text npcNameText;
    public Text dialogueText;

    void Start()
    {
        string path = Application.streamingAssetsPath + "/" + jsonFileName;
        string json = File.ReadAllText(path);
        Dialogue loadedDialogue = JsonUtility.FromJson<Dialogue>(json);
        // 会話UIに反映
        dialogueData = loadedDialogue;

        Debug.Log("Loaded dialogue for NPC: " + dialogueData.npcName);
        npcNameText.text = dialogueData.npcName;

        Debug.Log("First dialogue line: " + dialogueData.dialogue[0]);
        dialogueText.text = dialogueData.dialogue[0]; // 最初のセリフ
    }
}
