using UnityEngine;

public class RePosition : MonoBehaviour
{
    private float mapSize = 40;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        // �÷��̾��� ��ġ �Ÿ� 
        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        // �� �������� ��ġ
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                {
                    float diffX = playerPos.x - myPos.x;
                    float diffY = playerPos.y - myPos.y;
                    float dirX = diffX < 0 ? -1 : 1;
                    float dirY = diffY < 0 ? -1 : 1;

                    diffX = Mathf.Abs(diffX);
                    diffY = Mathf.Abs(diffY);

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
                        Vector3 dist = playerPos - myPos;
                        Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                        transform.Translate(random + dist * 2);
                    }
                }
                break;
        }
    }
}
