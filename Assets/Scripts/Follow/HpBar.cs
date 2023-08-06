using UnityEngine;

public class HpBar : MonoBehaviour
{
    private RectTransform rect;

    private float posY = 0.8f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        FollowHpBar();
    }

    private void FollowHpBar()
    {
        var camera = CameraManager.Instance.MainCamera();
        var player = PlayerManager.Instance.GetPlayer();
        var playerPos = player.transform.position;
        playerPos.y = player.transform.position.y - posY;
        rect.position = camera.WorldToScreenPoint(playerPos);
    }
}
