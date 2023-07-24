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

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;

        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                {
                    if (Mathf.Abs(diffX - diffY) <= 0.01f)
                    {
                        transform.Translate(Vector3.up * dirY * mapSize);
                        transform.Translate(Vector3.right * dirX * mapSize);
                    }
                    else if (diffX > diffY)
                    {
                        transform.Translate(Vector3.right * dirX * mapSize);
                    }
                    else if (diffX < diffY)
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
