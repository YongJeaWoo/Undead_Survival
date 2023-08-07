using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPrefab
{
    public string key;
    public Weapon weapon;
    public ItemData itemData;
}

public class WeaponManager : SingletonComponent<WeaponManager>
{
    // 할당하여 생성할 무기 프리팹
    [SerializeField] private List<WeaponPrefab> weapons;
    // 이름으로 생성할 무기
    private Dictionary<string, ItemData> weaponDic;

    private int maxWeapons = 5;

    // 회전 무기 리스트
    private List<RotateWeapon> activeWeapons = new List<RotateWeapon>();

    private void SetWeapons()
    {
        weaponDic = new Dictionary<string, ItemData>();

        foreach (var weaponPrefab in weapons)
        {
            weaponDic.Add(weaponPrefab.itemData.itemName, weaponPrefab.itemData);
        }
    }

    public Dictionary<string, ItemData> GetWeapon() => weaponDic;

    public void AddWeapon(string weaponName)
    {
        if (!weaponDic.ContainsKey(weaponName)) return;

        var pool = ObjectPoolManager.Instance.GetPool(weaponName);
        var transform = pool.GetActiveObject();
        ObjectPoolManager.Instance.Create(weaponName, transform);
    }

    public void AddRotateWeapon(string weaponName)
    {
        if (activeWeapons.Count >= maxWeapons) return;
        if (!weaponDic.ContainsKey(weaponName)) return;

        var pool = ObjectPoolManager.Instance.GetPool(weaponName);
        var transform = pool.GetActiveObject();
        var weapon = ObjectPoolManager.Instance.Create(weaponName, transform);
        var component = weapon.GetComponent<RotateWeapon>();
        activeWeapons.Add(component);

        UpdateWeaponPosition();
    }

    public void RemoveRotateWeapon()
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

    private void UpdateWeaponPosition()
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
