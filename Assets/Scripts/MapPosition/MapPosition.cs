using UnityEngine;

public class MapPosition : MonoBehaviour
{
    private float mapSize = 40;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        // 플레이어의 위치 거리 

        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        // 맵 포지션의 위치
        Vector3 myPos = transform.position;

        // 결과값은 양수로
        float posX = Mathf.Abs(playerPos.x - myPos.x);
        float posY = Mathf.Abs(playerPos.y - myPos.y);

        // 플레이어의 방향
        Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;

        // 플레이어의 방향 파악
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                {
                    if (posX > posY)
                    {
                        transform.Translate(Vector3.right * dirX * mapSize);
                    }
                    else if (posX < posY)
                    {
                        transform.Translate(Vector3.up * dirY * mapSize);
                    }
                }
                break;
            case "Enemy":
                {

                }
                break;
        }
    }
}
