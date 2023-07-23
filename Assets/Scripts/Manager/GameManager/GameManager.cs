using SingletonComponent.Component;

public class GameManager : SingletonComponent<GameManager>
{
    private void Start()
    {
        PlayerManager.Instance.InitManPlayer();
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
