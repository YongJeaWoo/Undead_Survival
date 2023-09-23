using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel 
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button getOutButton;

    private new void Awake()
    {
        base.Awake();
        hasAnimation = true;
    }

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClick);
        getOutButton.onClick.AddListener(OnGetoutButtonClick);
    }

    public override void Show()
    {
        UIManager.Instance.ChangeImage("Pause");
        LevelManager.Instance.Gamestate = E_GameState.Pause;
    }

    public override void Hide()
    {
        UIManager.Instance.ChangeImage("Setting");
        LevelManager.Instance.Gamestate = E_GameState.Resume;
    }

    public override void Exit()
    {
        UIManager.Instance.ChangeImage("Pause");
        LevelManager.Instance.Gamestate = E_GameState.Resume;
    }

    public void OnContinueButtonClick()
    {
        Hide();
    }

    public void OnGetoutButtonClick()
    {
        Exit();

        //SceneManager.LoadScene();
    }
}
