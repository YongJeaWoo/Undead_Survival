using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾� �Է� ��ġ
    private Vector2 inputVec;

    // �ӵ� ����
    private float moveSpeed = 3f;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

    #endregion
}
