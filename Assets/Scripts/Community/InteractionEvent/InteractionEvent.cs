using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] ChooseGetDialogue(int storyId)
    {
        Dictionary<int, List<Dialogue>> dialogues = DataBaseManager.Instance.GetDialogue();
        if (dialogues.ContainsKey(storyId))
        {
            dialogue.dialogues = dialogues[storyId].ToArray();
        }
        else
        {
            dialogue.dialogues = new Dialogue[0];
        }
        return dialogue.dialogues;
    }
}
