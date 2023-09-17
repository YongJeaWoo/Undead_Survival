using SingletonComponent.Component;
using System.Collections;
using TMPro;
using UnityEngine;


public class DialogueManager : SingletonComponent<DialogueManager>
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDialogue;

    private Dialogue[] dialogues;

    private bool isDialogue = false;
    private bool isNext = false;

    private int idCount = 0;
    private int contextCount = 0;

    private InteractionEvent iEvent;

    public GameObject DialogueCanvas { get => dialogueCanvas; }
    public InteractionEvent IEvent { get => iEvent; set => iEvent = value;  }

    private void Update()
    {
        InputKeys();
    }

    public void ShowDialogue(Dialogue[] dialogues)
    {
        isDialogue = true;
        textName.text = "";
        textDialogue.text = "";
        this.dialogues = dialogues;

        StartCoroutine(nameof(TypeWriter));
    }

    private void InputKeys()
    {
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isNext)
                {
                    isNext = true;
                }
                else
                {
                    isNext = false;
                    textDialogue.text = "";

                    if (++contextCount < dialogues[idCount].contexts.Length)
                    {
                        StartCoroutine(nameof(TypeWriter));
                    }
                    else
                    {
                        contextCount = 0;
                        idCount++;

                        if (idCount < dialogues.Length)
                        {
                            StartCoroutine(nameof(TypeWriter));
                        }
                        else
                        {
                            isDialogue = false;
                            LevelManager.Instance.Gamestate = E_GameState.Init;
                            UIManager.Instance.SettingUI(false);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator TypeWriter()
    {
        UIManager.Instance.SettingUI(true);

        string replaceText = dialogues[idCount].contexts[contextCount];
        replaceText = replaceText.Replace("'", ",");

        textName.text = dialogues[idCount].name;
        textDialogue.text = "";

        foreach (char letter in replaceText.ToCharArray())
        {
            if (isNext && isDialogue)
            {
                textDialogue.text = replaceText;
                break;
            }
            else
            {
                textDialogue.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }

        isNext = true;
    }

    #region Singleton

    protected override void AwakeInstance()
    {
        
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
