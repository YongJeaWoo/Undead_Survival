using UnityEngine;

public enum E_PlayerState
{
    Stand,
    Run,
    Dead
}

public class Player : MonoBehaviour
{
    // 플레이어 입력 방향
    private Vector2 inputVec;

    // 속도 관리
    private float moveSpeed = 5f;

    private Rigidbody2D rigid;
    private SpriteRenderer spRender;
    private AnimationController aniController;
    private Scanner scanner;

    [SerializeField] private Hand[] hands;

    #region Property

    public Vector2 InputVec
    {
        get => inputVec;
        set => inputVec = value;
    }
    public Rigidbody2D Rigid
    {
        get => rigid;
    }
    public Scanner Scan
    {
        get => scanner;
    }
    public Hand[] Hands
    {
        get => hands;
        set => hands = value;
    }

    #endregion

    private void Awake()
    {
        GetComponents();
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
        Dead();
    }

    private void OnEnable()
    {
        
    }

    private void GetComponents()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRender = GetComponent<SpriteRenderer>();
        aniController = GetComponent<AnimationController>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    #region Input Keys

    private void InputKeys()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedInputKeys()
    {
        var playerHP = PlayerManager.Instance.GetCurrentHealth();

        if (playerHP <= 0) return;
        // 움직임 균일 이동
        Vector2 moveVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + moveVec);
    }

    private void InputAnimation()
    {
        if (Time.timeScale == 0f) return;

        var playerHP = PlayerManager.Instance.GetCurrentHealth();

        if (playerHP <= 0) return;

        if (inputVec.x != 0 || inputVec.y != 0)
        {
            aniController.PlayerAnimation(E_PlayerState.Run);

            if (inputVec.x != 0)
            {
                spRender.flipX = inputVec.x < 0;
            }
        }
        else
        {
            aniController.PlayerAnimation(E_PlayerState.Stand);
        }
    }

    #endregion

    private void Dead()
    {
        var playerHP = PlayerManager.Instance.GetCurrentHealth();

        if (playerHP <= 0)
        {
            playerHP = 0;

            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            aniController.PlayerAnimation(E_PlayerState.Dead);
        }

        Debug.Log(playerHP);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerManager.Instance.DecreaseHealth(Time.deltaTime * 10);
    }
}
