using UnityEngine;

public class Bullet : Weapon
{
    private float timer = 0f;
    private string bulletName;

    private Scanner scanner;
    private WeaponManager weaponManager;

    public override void OnEnable()
    {
        base.OnEnable();
        scanner = playerTransform.GetComponent<Scanner>();
        weaponManager = WeaponManager.Instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > speed)
        {
            timer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        // if (scanner.Target == Vector3.zero) return;

        var direction = (scanner.Target - playerTransform.position).normalized;
        var bulletPrefabName = weaponManager.Weapon().Find(w => w.key == bulletName).weapon.name;
        var bullet = ObjectPoolManager.Instance.Create(bulletPrefabName);
        bullet.transform.position = playerTransform.position + direction;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
