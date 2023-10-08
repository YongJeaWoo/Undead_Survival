using UnityEngine;

public class TitleController : MonoBehaviour
{
    public void GameStartButton()
    {
        Loader.LoadScene("Game");
    }

    public void OptionButton()
    {
        PanelManager.Instance.TogglePanel<OptionPanel>();
    }
}
