using SingletonComponent.Component;
using UnityEngine;

public class EnemyManager : SingletonComponent<EnemyManager>
{
    private Enemy enemy;

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
