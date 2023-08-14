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

    public SpriteRenderer Spriter
    {
        get => spriteRenderer;
        set => spriteRenderer = value;
    }

    public bool IsLeft
    {
        get => isLeft;
    }

    private void LateUpdate()
    {
        CheckReverse();
    }

    private void CheckReverse()
    {
        var checkPlayer = PlayerManager.Instance.GetPlayer();
        var targetAppoint = checkPlayer.Scan.Target;
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 10;
        }
        // 손을 가장 가까운 적에게 타겟팅
        //else if (targetAppoint != null)
        //{
        //    Vector3 targetPos = checkPlayer.Scan.Target;
        //    Vector3 dir = targetPos - transform.position;
        //    transform.localRotation = Quaternion.FromToRotation(Vector3.right, dir);

        //    bool isRot1 = transform.localRotation.eulerAngles.z > 90 &&
        //        transform.localRotation.eulerAngles.z < 270;
        //    bool isRot2 = transform.localRotation.eulerAngles.z < -90 &&
        //        transform.localRotation.eulerAngles.z > -270;
        //    Spriter.flipY = isRot1 || isRot2;
        //    spriteRenderer.sortingOrder = 6;
        //}
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 10 : 4;
        }
    }

    public void InitAwake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentsInParent<SpriteRenderer>()[0];
    }
}
