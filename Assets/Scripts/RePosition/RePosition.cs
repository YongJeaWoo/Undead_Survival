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
        // �÷��̾��� ��ġ �Ÿ� 
        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        // �� �������� ��ġ
        Vector3 myPos = transform.position;

        // �÷��̾�� ��/���� �Ÿ��� ���� �Ÿ� �̻��� ��� ���ġ
        if (Vector3.Distance(playerPos, myPos) > someDistance)
        {
            // ���ġ ����
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
                        // ���� �� �ݶ��̴��� ������ �÷��̾� �������� ���ġ
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

    //    // �÷��̾��� ��ġ �Ÿ� 
    //    Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
    //    // �� �������� ��ġ
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
    //                // ���� �� �ݶ��̴��� ������ �÷��̾� �������� ���ġ
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
