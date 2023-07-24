using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPrefab
{
    public string key;
    public Player player;
}

public class PlayerManager : SingletonComponent<PlayerManager>
{
    // �ν����Ϳ� �Ҵ� ������ �÷��̾� ����Ʈ ���
    [SerializeField] private List<PlayerPrefab> playerPrefabs;
    // �̸����� ������ �÷��̾�
    private Dictionary<string, Player> playerPrefabDic;

    private Player player;

    public Player GetPlayer() => player;

    public void InitPlayer(string name)
    {
        player = Instantiate(playerPrefabDic[name]);
        CameraManager.Instance.InitCamera();
    }

    private void SetPlayers()
    {
        playerPrefabDic = new Dictionary<string, Player>();

        foreach (var playerPrefab in playerPrefabs)
        {
            playerPrefabDic.Add(playerPrefab.key, playerPrefab.player);
        }
    }

    #region Singleton

    protected override void AwakeInstance()
    {
        SetPlayers();
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
