using UnityEngine;

public enum E_EnemyState
{
    Run,
    Hit,
    Dead
}

public class Enemy : MonoBehaviour
{
    private float speed = 2.5f;

    private float health = 100f;

    // 넉백
    private float knockbackDuration = 0.05f;
    private float knockbackTimer = 0f;

    private float knockbackDistance = 1.5f;

    private Collider2D col;
    private Rigidbody2D targetRigid;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;

    private WeaponManager weaponManager;


    private void Awake()
    {
        GetComponents();
    }

    private void OnEnable()
    {
        Target();
    }

    private void FixedUpdate()
    {
        Tracking();
    }

    private void LateUpdate()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Attacker")) return;

        Weapon weapon = collision.GetComponent<Weapon>();
        if (weapon != null) OnAttacked(weapon);
    }

    private void GetComponents()
    {
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationController = GetComponent<AnimationController>();
    }

    private void Tracking()
    {
        var playerHP = PlayerManager.Instance.GetCurrentHealth();

        if (knockbackTimer <= 0)
        {
            if (playerHP > 0)
            {
                // 상대 위치와 나의 위치 거리
                Vector2 dirVec = targetRigid.position - rigid.position;
                Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                // 플레이어 위치로 움직이기
                animationController.EnemyAnimation(E_EnemyState.Run);
                rigid.MovePosition(rigid.position + nextVec);
                // 물리적 충돌 없애기
                rigid.velocity = Vector2.zero;
            }
        }
        else
        {
            knockbackTimer -= Time.fixedDeltaTime;
        }
    }

    private void Target()
    {
        col.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 3;

        targetRigid = PlayerManager.Instance.GetPlayer().Rigid;
        weaponManager = WeaponManager.Instance;
    }

    private void Rotate()
    {
        if (health < 0) return;

        spriteRenderer.flipX = targetRigid.position.x < rigid.position.x;
    }

    private void Dead()
    {
        col.enabled = false;
        // 물리적 비활성화
        rigid.simulated = false;
        animationController.EnemyAnimation(E_EnemyState.Dead);
        // 순서 변경
        spriteRenderer.sortingOrder = 1;

        Invoke(nameof(DeleteObject), 1.5f);
    }

    private void DeleteObject()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }

    private void Knockback()
    {
        Vector3 playerPos = PlayerManager.Instance.GetPlayer().transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rigid.AddForce(dirVec.normalized * knockbackDistance, ForceMode2D.Impulse);
        knockbackTimer = knockbackDuration;
    }

    public void SetHealth(int maxHealth)
    {
        health = maxHealth;
    }

    public void OnAttacked(Weapon weapon)
    {
        float damage = weapon.Damage;

        health -= damage;
        Knockback();

        if (health > 0)
        {
            animationController.EnemyAnimation(E_EnemyState.Hit);
        }
        else
        {
            Dead();
            GameManager.Instance.AddKill();
            GameManager.Instance.AddExperience(30);
        }
    }
}
