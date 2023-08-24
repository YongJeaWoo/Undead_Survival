using UnityEngine;

public class GameController : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.Gamestate = E_GameState.Ready;
    }

    private void Update()
    {
        if (LevelManager.Instance.Gamestate == E_GameState.Start)
        {
            LevelManager.Instance.SpawnEnemy();
        }
    }
}
