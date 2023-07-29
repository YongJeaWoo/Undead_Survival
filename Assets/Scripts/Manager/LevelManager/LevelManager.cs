using SingletonComponent.Component;
using UnityEngine;

public enum E_GameState 
{
    // 준비단계, 게임 시작, 레벨 변화, 게임 오버, 게임 클리어
    None,
    Ready,
    Start,
    Changing,
    GameOver,
    GameClear,
}


public class LevelManager : SingletonComponent<LevelManager>
{
    private float spawnTimer;
    private E_GameState gameState = E_GameState.None;


    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > 5f)
        {
            spawnTimer = 0f;
            EnemyManager.Instance.InitEnemy();
        }
    }

    

    // 상태 변화
    public void ChangeGameState(E_GameState newState)
    {
        gameState = newState;

        switch (gameState)
        {
            case E_GameState.Ready:
                {
                    PlayerManager.Instance.InitPlayer("Man Player");
                }
                break;
            case E_GameState.Start:
                {

                }
                break;
            case E_GameState.Changing:
                break;
            case E_GameState.GameOver:
                break;
            case E_GameState.GameClear:
                break;
        }
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
