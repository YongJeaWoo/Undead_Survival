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

    // �ν����Ϳ� �Ҵ� ������ �÷��̾� ����Ʈ ���
    [SerializeField] private List<PlayerPrefab> playerPrefabs;
    // �̸����� ������ �÷��̾�
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
        // ������ �÷��̾��� Hand Ŭ������ �ν��Ͻ��� ���� InitAwake �޼��� ȣ��
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
