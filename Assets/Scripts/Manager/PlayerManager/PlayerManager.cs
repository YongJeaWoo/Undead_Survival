using SingletonComponent.Component;
using UnityEngine;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    private Player player;

    [SerializeField] private Player manPlayer;
    [SerializeField] private Player womanPlayer;

    public Player GetPlayer() => player;

    public void InitManPlayer()
    {
        player = Instantiate(manPlayer);
        CameraManager.Instance.InitCamera();
    }

    public void InitWomanPlayer()
    {
        player = Instantiate(womanPlayer);
        CameraManager.Instance.InitCamera();
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
