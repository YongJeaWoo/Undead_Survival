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
    // 인스펙터에 할당 가능한 플레이어 리스트 목록
    [SerializeField] private List<PlayerPrefab> playerPrefabs;
    // 이름으로 생성할 플레이어
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
