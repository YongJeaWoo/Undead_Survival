using UnityEngine;

public enum E_AniState
{
    Stand,
    Run,
    Dead
}

public class Player : MonoBehaviour
{
    // �÷��̾� �Է� ��ġ
    private Vector2 inputVec;

    // �ӵ� ����
    private float moveSpeed = 3f;

    private Rigidbody2D rigid;
    private SpriteRenderer spRender;
    private AnimationController aniController;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRender = GetComponent<SpriteRenderer>();
        aniController = GetComponent<AnimationController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        InputKeys();
    }

    private void FixedUpdate()
    {
        FixedInputKeys();
    }

    private void LateUpdate()
    {
        InputAnimation();
    }

    #region Input Keys

    private void InputKeys()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedInputKeys()
    {
        // ������ ���� �̵�
        Vector2 moveVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + moveVec);
    }

    private void InputAnimation()
    {
        if (inputVec.x != 0 || inputVec.y != 0)
        {
            aniController.AnimationPlay(E_AniState.Run);

            if (inputVec.x != 0)
            {
                spRender.flipX = inputVec.x < 0;
            }
        }
        else
        {
            aniController.AnimationPlay(E_AniState.Stand);
        }
    }

    #endregion
}
