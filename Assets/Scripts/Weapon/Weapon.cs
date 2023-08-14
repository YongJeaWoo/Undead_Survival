using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float speed;

    public float defaultAngle = 0f;
    public float accumulateAngle = 0f;

    public float centerDistance = 1.5f;

    public ItemData data;

    protected Transform playerTransform;
    protected WeaponManager weaponManager;

    public float CenterDistance() => centerDistance;

    public virtual void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
        weaponManager = WeaponManager.Instance;

        var item = Item.Instance;
        if (item != null)
        {
            damage = data.baseDamage;
            speed = data.baseSpeed;
        }
    }
    
    public virtual void Init()
    {
        damage = data.baseDamage;
        speed = data.baseSpeed;
    }

    public virtual void SetDefaultAngle(float _angle)
    {
        defaultAngle = _angle;
    }

    public virtual void SetCenterDistance(float distance)
    {
        centerDistance = distance;
    }

    public void LevelUp(int level)
    {
        damage = data.baseDamage + data.levelDamage[level];
        speed = data.baseSpeed + data.levelSpeed[level];
    }
}
