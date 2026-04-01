using UnityEngine;

[CreateAssetMenu(fileName = "DialogueEventSO", menuName = "Game/DialogueEventSO")]
public class DialogueEventSO : ScriptableObject
{
    public string dialogueID;
    public string conversation;
    public bool seen; //runtime flag
}
