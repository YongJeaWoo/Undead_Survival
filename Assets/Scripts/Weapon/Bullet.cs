using UnityEngine;

public class Bullet : Weapon
{
    private Vector3 targetPosition;

    public override void OnEnable()
    {
        base.OnEnable();
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
        transform.position += targetPosition * Speed * Time.deltaTime;
    }

    public override void Init(WeaponData data)
    {
        base.Init(data);
        centerDistance = 1;
    }

    public void SetTarget(Vector3 target) => targetPosition = (target - transform.position).normalized;
}
