using SingletonComponent.Component;
using UnityEngine;

public enum E_GameState 
{
    // �غ�ܰ�, ���� ����, ���� �Ͻ� ����, ���� �簳, ���� ��ȭ, ���� ����, ���� Ŭ����
    None,
    Ready,
    Explain,
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

    public void ButtonClickToExplain()
    {
        Gamestate = E_GameState.Explain;
    }

    public void ButtonClickToStart()
    {
        Gamestate = E_GameState.Start;
    }

    // ���� ��ȭ
    private void ChangeGameState(E_GameState newState)
    {
        gameState = newState;

        switch (gameState)
        {
            case E_GameState.Ready:
                {
                    DialogueManager.Instance.OnSelectPlayerBox(true);
                }
                break;
            case E_GameState.Explain:
                {
                    DialogueManager.Instance.OnSelectPlayerBox(false);
                    DialogueManager.Instance.SettingUI(true);
                }
                break;
            case E_GameState.Start:
                {
                    CameraManager.Instance.InitCamera();
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
