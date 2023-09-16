using SingletonComponent.Component;

public class PanelManager : SingletonComponent<PanelManager>
{
    #region Property

    #endregion

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

    #region Public Methods

    public void TogglePausePanel()
    {
        var pausePanel = PopupManager.Instance.Find<PausePanel>();

        if (pausePanel != null)
        {
            pausePanel.Hide();
            PopupManager.Instance.RemovePopUp<PausePanel>();
        }
        else
        {
            pausePanel = PopupManager.Instance.GetPopUp<PausePanel>();
            pausePanel.Show();
        }
    }

    #endregion
}
