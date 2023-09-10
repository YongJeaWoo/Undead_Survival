using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private string PATH_FILE = "Dialogue/";

    public Dictionary<int, List<Dialogue>> Parse(string _csvFileName)
    {
        Dictionary<int, List<Dialogue>> dialogues = new Dictionary<int, List<Dialogue>>();
        TextAsset csvData = Resources.Load<TextAsset>($"{PATH_FILE}{_csvFileName}");

        string[] lines = csvData.text.Split('\n');
        string prevCharacterName = "";
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            int storyId = int.Parse(fields[0]);
            string characterName = fields[2];
            if (string.IsNullOrWhiteSpace(characterName))
            {
                characterName = prevCharacterName;
            }
            else
            {
                prevCharacterName = characterName;
            }

            string dialogueText = fields[3].Replace("|", "\n");

            if (!dialogues.ContainsKey(storyId))
            {
                dialogues[storyId] = new List<Dialogue>();
            }

            Dialogue dialogue;
            if (dialogues[storyId].Count > 0 && dialogues[storyId].Last().name == characterName)
            {
                dialogue = dialogues[storyId].Last();
                List<string> contexts = dialogue.contexts.ToList();
                contexts.Add(dialogueText);
                dialogue.contexts = contexts.ToArray();
            }
            else
            {
                dialogue = new Dialogue();
                dialogue.name = characterName;
                dialogue.contexts = new string[] { dialogueText };
                dialogues[storyId].Add(dialogue);
            }
        }

        return dialogues;
    }
}
