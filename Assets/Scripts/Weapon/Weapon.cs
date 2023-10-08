using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected WeaponManager weaponManager;
    protected PlayerManager playerManager;
    protected WeaponData prefabData;

    public float Damage => prefabData.Damage;
    public float Speed => prefabData.Speed;

    public float defaultAngle = 0f;
    public float accumulateAngle = 0f;
    public float centerDistance = 1.5f;

    public float CenterDistance() => centerDistance;

    public virtual void OnEnable()
    {
        weaponManager = WeaponManager.Instance;
        playerManager = PlayerManager.Instance;
    }
    
    public virtual void Init(WeaponData data)
    {
        prefabData = data;

        name = $"Weapon {prefabData.itemData.name}";
        transform.parent = playerManager.GetPlayer().transform;
        transform.localPosition = Vector3.zero;
    }

    public virtual void SetDefaultAngle(float _angle)
    {
        defaultAngle = _angle;
    }

    public virtual void SetCenterDistance(float distance)
    {
        centerDistance = distance;
    }
}
