using System.Collections.Generic;
using UnityEngine;

public class InfoHUD : MonoBehaviour
{
    [SerializeField] private Transform upgradeBtnParent = null;
    [SerializeField] private WeaponUpgradeButton upgradeBtnPrefab;

    private List<WeaponData> weapons = null;
    private List<WeaponUpgradeButton> upgradeButtons = new List<WeaponUpgradeButton>();

    private void Start()
    {
        InitializeUpgradeButtons();
    }

    private void InitializeUpgradeButtons()
    {
        weapons ??= WeaponManager.Instance.GetWeaponDataList();

        for (var i = 0; i < weapons.Count; i++)
        {
            var create = Instantiate(upgradeBtnPrefab, upgradeBtnParent);
            create.transform.localScale = Vector3.one;
            create.InitItem(weapons[i]);

            upgradeButtons.Add(create);
        }
    }
}
