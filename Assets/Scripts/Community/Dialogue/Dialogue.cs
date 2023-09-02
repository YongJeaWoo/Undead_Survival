using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("��ȭ�ϴ� �̸�")]
    public string name;
    [Tooltip("��ȭ ����")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    // ��� �̺�Ʈ����
    public string eventName;
    // �پ��� ��ȭ�� �迭��
    public Dialogue[] dialogues;
}
