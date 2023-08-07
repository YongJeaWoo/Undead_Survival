using SingletonComponent.Component;
using UnityEngine;

public enum E_GameState 
{
    // 준비단계, 게임 시작, 게임 일시 정지, 게임 재개, 레벨 변화, 게임 오버, 게임 클리어
    None,
    Ready,
    Start,
    Pause,
    Resume,
    Changing,
    GameOver,
    GameClear,
}


public class LevelManager : SingletonComponent<LevelManager>
{
    private float spawnTimer;
    private E_GameState gameState = E_GameState.None;

    public E_GameState Gamestate
    {
        get => gameState;
        set => ChangeGameState(value);
    }

    public void SpawnEnemy()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > 3f)
        {
            spawnTimer = 0f;
            EnemyManager.Instance.InitEnemy();
        }
    }

    // 상태 변화
    private void ChangeGameState(E_GameState newState)
    {
        gameState = newState;

        switch (gameState)
        {
            case E_GameState.Ready:
                {
                    PlayerManager.Instance.InitPlayer("Man Player");

                    // 생성된 플레이어의 Hand 클래스의 인스턴스에 대해 InitAwake 메서드 호출
                    foreach (var hand in PlayerManager.Instance.GetPlayer().Hands)
                    {
                        hand.InitAwake();
                    }
                }
                break;
            case E_GameState.Start:
                {

                }
                break;
            case E_GameState.Pause:
                {
                    Time.timeScale = 0f;
                }
                break;
            case E_GameState.Resume:
                {
                    Time.timeScale = 1f;
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
