using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPrefab
{
    public string key;
    public Weapon weapon;
}

public class WeaponManager : SingletonComponent<WeaponManager>
{
    // 할당하여 생성할 무기 프리팹
    [SerializeField] private List<WeaponPrefab> weapons;
    // 이름으로 생성할 무기
    private Dictionary<string, Weapon> weaponPrefabDic;

    private int maxWeapons = 5;
    private float weaponSpacing;

    private List<Weapon> activeWeapons = new List<Weapon>();

    public List<WeaponPrefab> Weapon() => weapons;

    private void SetWeapons()
    {
        weaponPrefabDic = new Dictionary<string, Weapon>();

        foreach (var weaponPrefab in weapons)
        {
            weaponPrefabDic.Add(weaponPrefab.key, weaponPrefab.weapon);
        }
    }

    public void AddWeapon(string weaponName)
    {
        if (activeWeapons.Count >= maxWeapons) return;
        if (!weaponPrefabDic.ContainsKey(weaponName)) return;

        var weapon = ObjectPoolManager.Instance.Create(weaponName, transform).GetComponent<Weapon>();
        activeWeapons.Add(weapon);

        UpdateWeaponPosition();
    }

    public void RemoveWeapon()
    {
        if (activeWeapons.Count == 0) return;

        var weapon = activeWeapons[activeWeapons.Count - 1];
        activeWeapons.RemoveAt(activeWeapons.Count - 1);

        ObjectPoolManager.Instance.Return(weapon.gameObject);

        UpdateWeaponPosition();
    }

    // 추후 동적 플레이어와 무기 간격 배치
    public void SetWeaponSpacing(float distance)
    {
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            var weapon = activeWeapons[i];
            weapon.SetCenterDistance(distance);
        }

        UpdateWeaponPosition();
    }

    public void UpdateWeaponPosition()
    {
        var angleSpacing = 360f / activeWeapons.Count;
        for (int i = 0; i < activeWeapons.Count; i++)
        {
            var weapon = activeWeapons[i];
            var angle = angleSpacing * i;
            var offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * weapon.CenterDistance();
            weapon.transform.position = PlayerManager.Instance.GetPlayer().transform.position + offset;
            weapon.SetDefaultAngle(angle);
        }
    }

    #region Singleton

    protected override void AwakeInstance()
    {
        SetWeapons();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion
}
