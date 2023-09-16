using SingletonComponent.Component;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonComponent<UIManager>
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
