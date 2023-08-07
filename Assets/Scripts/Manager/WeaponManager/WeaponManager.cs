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
    // �Ҵ��Ͽ� ������ ���� ������
    [SerializeField] private List<WeaponPrefab> weapons;
    // �̸����� ������ ����
    private Dictionary<string, ItemData> weaponDic;

    private int maxWeapons = 5;

    // ȸ�� ���� ����Ʈ
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

    // ���� ���� �÷��̾�� ���� ���� ��ġ
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
