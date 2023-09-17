using UnityEngine;

public class PausePanel : MonoBehaviour 
{
    private RectTransform rectTrans;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    public void Show()
    {
        rectTrans.anchoredPosition = Vector2.zero;
        UIManager.Instance.ChangeImage("Pause");
        LevelManager.Instance.Gamestate = E_GameState.Pause;
    }

    public void Hide()
    {
        UIManager.Instance.ChangeImage("Setting");
        LevelManager.Instance.Gamestate = E_GameState.Resume;
    }
}
