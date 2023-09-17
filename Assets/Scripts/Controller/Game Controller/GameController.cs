using UnityEngine;

public class GameController : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.Gamestate = E_GameState.Ready;
    }

    private void Update()
    {
        StartSpawnEnemies();
        CheckGameEnd();
        InputPause();
    }

    private void StartSpawnEnemies()
    {
        if (LevelManager.Instance.Gamestate == E_GameState.Start)
        {
            LevelManager.Instance.SpawnEnemy();
        }
    }

    private void InputPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            (LevelManager.Instance.Gamestate == E_GameState.Start || 
            LevelManager.Instance.Gamestate == E_GameState.Pause))
        {
            PanelManager.Instance.TogglePausePanel();
        }
    }

    private void CheckGameEnd()
    {
        var gameManager = GameManager.Instance;
        var playerManager = PlayerManager.Instance;

        if (gameManager.GetCurrentTime() <= 0)
        {
            LevelManager.Instance.Gamestate = E_GameState.GameClear;
        }

        if (playerManager.GetCurrentHealth() <= 0)
        {
            LevelManager.Instance.Gamestate = E_GameState.GameOver;
        }
    }

    private void SelectionButton(E_PlayerType playerType)
    {
        PlayerManager.Instance.InitPlayer(playerType);

        InteractionEvent interactionEvent = DialogueManager.Instance.IEvent;

        if (interactionEvent != null)
        {
            switch (playerType)
            {
                case E_PlayerType.ManPlayer:
                    {
                        Dialogue[] dialogues = interactionEvent.ChooseGetDialogue(1);
                        DialogueManager.Instance.ShowDialogue(dialogues);
                    }
                    break;
                case E_PlayerType.WomanPlayer:
                    {
                        Dialogue[] dialogues = interactionEvent.ChooseGetDialogue(2);
                        DialogueManager.Instance.ShowDialogue(dialogues);
                    }
                    break;
            }
        }
    }

    public void ManPlayerButton()
    {
        SelectionButton(E_PlayerType.ManPlayer);
    }

    public void WomanPlayerButton()
    {
        SelectionButton(E_PlayerType.WomanPlayer);
    }
}
