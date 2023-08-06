using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer player;

    private Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    private Vector3 rightPosReverse = new Vector3(0.17f, -0.15f, 0);

    private Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    private Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        InitAwake();

    }

    private void LateUpdate()
    {
        CheckReverse();
    }

    private void CheckReverse()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 10;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 10 : 4;
        }
    }

    private void InitAwake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }
}
