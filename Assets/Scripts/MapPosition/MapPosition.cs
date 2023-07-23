using UnityEngine;

public class MapPosition : MonoBehaviour
{
    private float mapSize = 40;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        // �÷��̾��� ��ġ �Ÿ� 

        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        // �� �������� ��ġ
        Vector3 myPos = transform.position;

        // ������� �����
        float posX = Mathf.Abs(playerPos.x - myPos.x);
        float posY = Mathf.Abs(playerPos.y - myPos.y);

        // �÷��̾��� ����
        Vector3 playerDir = PlayerManager.Instance.GetPlayer().InputVec;

        // �÷��̾��� ���� �ľ�
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
