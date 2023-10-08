using SingletonComponent.Component;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    private List<Weapon> weapons = new List<Weapon>();

    public Weapon weaponPrefab;
    public ItemData itemData;

    public int currentLevel;

    public int CurrentLevel { get => currentLevel; }

    public int Count => weapons.Count;

    public string Name => itemData.name;

    public float Damage
    {
        get
        {
            float nextDamage = itemData.baseDamage;
            nextDamage += itemData.baseDamage * itemData.levelDamage[currentLevel];

            return nextDamage;
        }
    }

    public float Speed
    {
        get
        {
            float nextSpeed = itemData.baseSpeed;
            nextSpeed += itemData.baseSpeed * itemData.levelSpeed[currentLevel];
            return nextSpeed;
        }
    }

    public void AddLevel(int level)
    {
        currentLevel += level;
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    public Weapon CreateWeapon()
    {
        var weapon = ObjectPoolManager.Instance.Create(Name).GetComponent<Weapon>();
        weapon.Init(this);

        weapons.Add(weapon);

        return weapon;
    }

    public List<Weapon> GetWeaponList() => weapons;
}

public class WeaponManager : SingletonComponent<WeaponManager>
{
    // 이름으로 생성할 무기
    [SerializeField] private List<WeaponData> weaponDataList;
    private Dictionary<string, WeaponData> weaponDic;

    public WeaponData GetWeaponData(string weaponName)
    {
        if (weaponDic.TryGetValue(weaponName, out var result))
        {
            return result;
        }

        var data = weaponDataList.FirstOrDefault(x => x.Name == weaponName);
        weaponDic.Add(weaponName, data);

        return data;
    }

    public List<WeaponData> GetWeaponDataList() => weaponDataList;

    #region Singleton

    protected override void AwakeInstance()
    {

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
