using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public bool hasAnimation = false;
    public Vector2 showPosition;
    public Vector2 hidePosition;

    public void Awake()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();

        hidePosition = new Vector2(0, 0);
        showPosition = new Vector2(0, rectTrans.rect.height);
    }

    public abstract void Show();
    public abstract void Hide();
    public abstract void Exit();
}
