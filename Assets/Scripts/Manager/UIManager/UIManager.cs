using SingletonComponent.Component;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonComponent<UIManager>
{
    private const string PAUSEICON = "Sprites/Icon";

    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject selectPlayerBox;
    [SerializeField] private GameObject hud;

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

    public void SettingUI(bool flag)
    {
        DialogueManager.Instance.DialogueCanvas.SetActive(flag);
        hud.SetActive(!flag);
    }
}
