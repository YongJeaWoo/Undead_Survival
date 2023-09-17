using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public enum E_PlayerType
{
    ManPlayer,
    WomanPlayer,
}

[System.Serializable]
public class PlayerPrefab
{
    public string key;
    public Player player;
}

public class PlayerManager : SingletonComponent<PlayerManager>
{
    private float health;
    private float maxHealth = 100;

    // 인스펙터에 할당 가능한 플레이어 리스트 목록
    [SerializeField] private List<PlayerPrefab> playerPrefabs;
    // 이름으로 생성할 플레이어
    private Dictionary<string, Player> playerPrefabDic;

    private Player player;

    public Player GetPlayer() => player;

    public float GetCurrentHealth() => health;
    public float GetMaxHealth() => maxHealth;

    public void InitPlayer(E_PlayerType playerType)
    {
        health = maxHealth;
        switch (playerType)
        {
            case E_PlayerType.ManPlayer:
                {
                    player = Instantiate(playerPrefabDic["Man Player"]);
                }
                break;
            case E_PlayerType.WomanPlayer:
                {
                    player = Instantiate(playerPrefabDic["Woman Player"]);
                }
                break;
        }

        SetHands();
    }

    private void SetPlayers()
    {
        playerPrefabDic = new Dictionary<string, Player>();

        foreach (var playerPrefab in playerPrefabs)
        {
            playerPrefabDic.Add(playerPrefab.key, playerPrefab.player);
        }
    }

    private void SetHands()
    {
        // 생성된 플레이어의 Hand 클래스의 인스턴스에 대해 InitAwake 메서드 호출
        foreach (var hand in player.Hands)
        {
            hand.InitAwake();
        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
    }

    public void DecreaseHealth(float amount)
    {
        health -= amount;
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
