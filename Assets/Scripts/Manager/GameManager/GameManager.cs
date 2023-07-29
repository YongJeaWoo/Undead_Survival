using SingletonComponent.Component;

public class GameManager : SingletonComponent<GameManager>
{
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
