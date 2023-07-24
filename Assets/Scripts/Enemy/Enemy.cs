using UnityEngine;

public enum E_EnemyState
{
    Run,
    Dead
}

public class Enemy : MonoBehaviour
{
    private float speed = 2.5f;

    private Rigidbody2D targetRigid;
    private Rigidbody2D rigid;
    private SpriteRenderer spRender;
    private AnimationController aniController;

    private bool isLive;

    private void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        Target();
    }

    private void GetComponents()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRender = GetComponent<SpriteRenderer>();
        aniController = GetComponent<AnimationController>();
    }

    private void Target()
    {
        targetRigid = PlayerManager.Instance.GetPlayer().Rigid;
    }

    private void FixedUpdate()
    {
        // ��� ��ġ�� ���� ��ġ �Ÿ�
        Vector2 dirVec = targetRigid.position - rigid.position;        
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // �÷��̾� ��ġ�� �����̱�
        rigid.MovePosition(rigid.position + nextVec);
        // ������ �浹 ���ֱ�
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spRender.flipX = targetRigid.position.x < rigid.position.x;
    }
}
