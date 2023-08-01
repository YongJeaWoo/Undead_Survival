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
    private float health = 100f;

    private void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        Target();
    }

    private void FixedUpdate()
    {
        // 상대 위치와 나의 위치 거리
        Vector2 dirVec = targetRigid.position - rigid.position;        
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // 플레이어 위치로 움직이기
        rigid.MovePosition(rigid.position + nextVec);
        // 물리적 충돌 없애기
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spRender.flipX = targetRigid.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Attacker")) return;

        collision.SendMessage("OnAttacked", this);
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

    public void SetHealth(int maxHealth)
    {
        health = maxHealth;
    }

    public void OnAttacked(RotateWeapon weapon)
    {
        health -= weapon.Damage();

        if (health <= 0)
        {
            ObjectPoolManager.Instance.Return(gameObject);
        }
    }
}
