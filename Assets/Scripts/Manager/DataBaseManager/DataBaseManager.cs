using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : SingletonComponent<DataBaseManager>
{
    [SerializeField] private string csvFileName;

    private Dictionary<int, List<Dialogue>> dialogueDic = new Dictionary<int, List<Dialogue>>();

    public static bool isFinish = false;

    public string CSVFileName
    {
        get => csvFileName;
        set => csvFileName = value;
    }

    private void InitAwake()
    {
        DialogueParser theParser = GetComponent<DialogueParser>();
        Dictionary<int, List<Dialogue>> dialogues = theParser.Parse(csvFileName);

        foreach (var story in dialogues)
        {
            int storyId = story.Key;
            foreach (var dialogue in story.Value)
            {
                if (!dialogueDic.ContainsKey(storyId))
                {
                    dialogueDic.Add(storyId, new List<Dialogue>());
                }

                dialogueDic[storyId].Add(dialogue);
            }
        }

        isFinish = true;
    }

    public Dictionary<int, List<Dialogue>> GetDialogue() => dialogueDic;

    #region Singleton

    protected override void AwakeInstance()
    {
        InitAwake();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion
}
