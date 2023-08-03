using System.Collections.Generic;
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

    private WeaponManager weaponManager;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Attacker")) return;

        OnAttacked();
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
        weaponManager = WeaponManager.Instance;
    }

    public void SetHealth(int maxHealth)
    {
        health = maxHealth;
    }

    public void OnAttacked()
    {
        List<WeaponPrefab> weapons = weaponManager.Weapon();

        // �����ؾ���
        WeaponPrefab weapon = weapons[0];

        float damage = weapon.damage;
        float speed = weapon.speed;

        health -= damage;
        Debug.Log($"���� ü�� : {health} / ���� ������ {damage}");

        if (health <= 0)
        {
            ObjectPoolManager.Instance.Return(gameObject);
        }
    }
}
