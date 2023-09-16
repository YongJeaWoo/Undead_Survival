using SingletonComponent.Component;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : SingletonComponent<DialogueManager>
{
    private const string PAUSEICON = "Sprites/Icon";

    [SerializeField] private GameObject dialogueArea;
    [SerializeField] private GameObject selectPlayerBox;
    [SerializeField] private GameObject hud;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDialogue;

    private Dialogue[] dialogues;

    private bool isDialogue = false;
    private bool isNext = false;

    private int idCount = 0;
    private int contextCount = 0;

    private InteractionEvent iEvent;

    public InteractionEvent IEvent { get => iEvent; }

    private void OnEnable()
    {
        iEvent = FindObjectOfType<InteractionEvent>();
    }

    private void Update()
    {
        InputKeys();
    }

    public void ChangeImage(string iconName)
    {
        var newSprite = Resources.Load<Sprite>($"{PAUSEICON}{iconName}");

        if (newSprite != null)
        {
            pauseButton.GetComponent<Image>().sprite = newSprite;
        }
        else
        {
            Debug.LogError($"Failed to load sprite.");
        }
    }

    public void OnSelectPlayerBox(bool select)
    {
        selectPlayerBox.SetActive(select);
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
                            LevelManager.Instance.Gamestate = E_GameState.Start;
                            SettingUI(false);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator TypeWriter()
    {
        SettingUI(true);

        string replaceText = dialogues[idCount].contexts[contextCount];
        replaceText = replaceText.Replace("'", ",");

        textName.text = dialogues[idCount].name;
        textDialogue.text = ""; ;

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

    public void SettingUI(bool flag)
    {
        dialogueArea.SetActive(flag);
        hud.SetActive(!flag);
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
