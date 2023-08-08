using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float speed;
    public float centerDistance = 1.5f;

    public ItemData data;

    protected Transform playerTransform;
    protected WeaponManager weaponManager;

    public virtual void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
        weaponManager = WeaponManager.Instance;
    }

    public virtual void Init()
    {
        damage = data.baseDamage;
        speed = data.baseSpeed;
    }

    public void LevelUp(int level)
    {
        damage = data.levelDamage[level];
        speed = data.levelSpeed[level];
    }
}
