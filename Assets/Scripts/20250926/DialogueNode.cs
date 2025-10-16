using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextNodeID;
}

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public int nodeID;
    public string dialogueText;
    public List<DialogueChoice> choices;
}
