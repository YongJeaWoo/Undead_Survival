using SingletonComponent.Data;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus
{
    public List<IWeaponMethod> weapons = new List<IWeaponMethod>();
    public int ATK;
    public int DEF;
    //...

    public void RefreshAll()
    {
        weapons.ForEach(x => x.Refresh());
    }
}

public interface IWeaponMethod
{
    void Refresh();
}

public class TestManager : SingletonComponent<TestManager>
{
    private Dictionary<string, WeaponStatus> dicWeaponStatus = new Dictionary<string, WeaponStatus>();

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    public WeaponStatus GetWeaponStatus(string _weaponName)
    {
        return dicWeaponStatus.TryGetValue(_weaponName, out var r) ? r : null;
    }

    public void RefreshWeaponStatusAll(string _weaponName)
    {
        var status = GetWeaponStatus(_weaponName);

        if (null == status) return;

        status.RefreshAll();
    }

    public void AddWeapon(string _weaponName, IWeaponMethod _weapon)
    {
        var status = GetWeaponStatus(_weaponName);

        if (null == status) return;

        status.weapons.Add(_weapon);
    }

    public void RemoveWeapon(string _weaponName, IWeaponMethod _weapon)
    {
        var status = GetWeaponStatus(_weaponName);

        if (null == status) return;

        status.weapons.Remove(_weapon);
    }
}

public class tttt : MonoBehaviour, IWeaponMethod
{
    private WeaponStatus statusData;

    public void Refresh()
    {
        Initialize();
    }

    private void OnEnable()
    {
        TestManager.Instance.AddWeapon(nameof(tttt), this);
    }

    private void OnDisable()
    {
        TestManager.Instance.RemoveWeapon(nameof(tttt), this);
    }

    public void Initialize()
    {
        statusData = TestManager.Instance.GetWeaponStatus(nameof(tttt));
    }
}
