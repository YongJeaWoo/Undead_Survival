using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private string enemy = "Enemy";

    private float fireDelay = 3f;
    private float fireTimer = 0f;
    private float scanRange = 8f;
    private LayerMask enemyLayer;

    private Transform playerTransform;
    private Transform target;

    private WeaponManager weaponManager;

    #region Property

    public Vector3 Target => target != null ? target.position : Vector3.zero;

    #endregion

    private void OnEnable()
    {
        playerTransform = transform;
        weaponManager = WeaponManager.Instance;
        enemyLayer = 1 << LayerMask.NameToLayer(enemy);
    }

    private void Update()
    {
        ScanForEnemies();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireDelay)
        {
            Fire();
            fireTimer = 0;
        }
    }

    private void ScanForEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, scanRange, enemyLayer);

        if (colliders.Length > 0)
        {
            target = colliders.OrderBy(c => Vector3.Distance(playerTransform.position, c.transform.position)).First().transform;
        }
        else
        {
            target = null;
        }
    }

    private void Fire()
    {
        if (target == null) return;

        GameObject bulletObject = ObjectPoolManager.Instance.Create("Bullet");
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        Vector3 direction = (target.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        bullet.transform.position = playerTransform.position;
        bullet.SetTarget(target.position);
    }
}
