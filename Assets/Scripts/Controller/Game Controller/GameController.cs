using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    WaitForSeconds waitSeconds = new WaitForSeconds(2f);

    private void OnEnable()
    {
        LevelManager.Instance.Gamestate = E_GameState.Ready;
    }

    private void Update()
    {
        LevelManager.Instance.SpawnEnemy();
    }
}
