using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("대화하는 이름")]
    public string name;
    [Tooltip("대화 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    // 어떠한 이벤트인지
    public string eventName;
    // 다양한 대화를 배열로
    public Dialogue[] dialogues;
}
