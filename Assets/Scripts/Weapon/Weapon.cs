using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float centerDistance = 1.5f;

    protected Transform playerTransform;
    protected WeaponManager weaponManager;


    public virtual void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
        weaponManager = WeaponManager.Instance;
    }
}
