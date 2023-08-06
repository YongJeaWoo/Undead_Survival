using UnityEngine;

public class RePosition : MonoBehaviour
{
    private float mapSize = 40;

    private float someDistance = 10f;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        RePositionObject();
    }

    private void RePositionObject()
    {
        // 플레이어의 위치 거리 
        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        // 맵 포지션의 위치
        Vector3 myPos = transform.position;

        // 플레이어와 맵/적의 거리가 일정 거리 이상일 경우 재배치
        if (Vector3.Distance(playerPos, myPos) > someDistance)
        {
            // 재배치 로직
            // ...
            switch (transform.tag)
            {
                case "Ground":
                    {
                        float diffX = Mathf.Abs(playerPos.x - myPos.x);
                        float diffY = Mathf.Abs(playerPos.y - myPos.y);

                        Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;

                        float dirX = playerDir.x < 0 ? -1 : 1;
                        float dirY = playerDir.y < 0 ? -1 : 1;

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
                        // 적이 맵 콜라이더에 닿으면 플레이어 방향으로 재배치
                        if (col.enabled)
                        {
                            Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;
                            float random = Random.Range(-3f, 3f);

                            transform.Translate(playerDir * (mapSize * 1.2f) + new Vector3(random, random, 0));
                        }
                    }
                    break;
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Area")) return;

    //    // 플레이어의 위치 거리 
    //    Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
    //    // 맵 포지션의 위치
    //    Vector3 myPos = transform.position;

    //    float diffX = Mathf.Abs(playerPos.x - myPos.x);
    //    float diffY = Mathf.Abs(playerPos.y - myPos.y);

    //    Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;

    //    float dirX = playerDir.x < 0 ? -1 : 1;
    //    float dirY = playerDir.y < 0 ? -1 : 1;

    //    switch (transform.tag)
    //    {
    //        case "Ground":
    //            {
    //                if (Mathf.Abs(diffX - diffY) <= 0.01f)
    //                {
    //                    transform.Translate(Vector3.up * dirY * mapSize);
    //                    transform.Translate(Vector3.right * dirX * mapSize);
    //                }
    //                else if (diffX > diffY)
    //                {
    //                    transform.Translate(Vector3.right * dirX * mapSize);
    //                }
    //                else if (diffX < diffY)
    //                {
    //                    transform.Translate(Vector3.up * dirY * mapSize);
    //                }
    //            }
    //            break;
    //        case "Enemy":
    //            {
    //                // 적이 맵 콜라이더에 닿으면 플레이어 방향으로 재배치
    //                if (col.enabled)
    //                {
    //                    float random = Random.Range(-3f, 3f);

    //                    transform.Translate(playerDir * (mapSize * 1.2f) + new Vector3(random, random, 0));
    //                }
    //            }
    //            break;
    //    }
    //}
}
