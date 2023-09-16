using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        UIManager.Instance.ChangeImage("Pause");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ChangeImage("Setting");
    }
}
