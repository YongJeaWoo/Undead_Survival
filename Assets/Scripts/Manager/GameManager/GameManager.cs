using SingletonComponent.Component;

public class GameManager : SingletonComponent<GameManager>
{
    private void Start()
    {
        PlayerManager.Instance.InitPlayer("Man Player");
        EnemyManager.Instance.InitEnemy();
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
