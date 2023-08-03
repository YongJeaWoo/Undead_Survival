using UnityEditor;
using UnityEngine;

public class Bullet : Weapon
{
    private Vector3 targetPosition;
    private float speed;

    public override void OnEnable()
    {
        base.OnEnable();
        WeaponPrefab bullet = weaponManager.GetWeapon("Bullet");
        speed = bullet.speed;
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void OnBecameInvisible()
    {
        Invoke(nameof(ReturnBullet), 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) ReturnBullet();
    }

    private void ReturnBullet()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }

    private void MoveTowardsTarget()
    {
        transform.position += targetPosition * speed * Time.deltaTime;
    }

    public void SetTarget(Vector3 target) => targetPosition = (target - transform.position).normalized;
}
