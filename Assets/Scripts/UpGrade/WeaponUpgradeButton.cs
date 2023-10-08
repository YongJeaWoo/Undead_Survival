using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeButton : MonoBehaviour
{
    private WeaponData weaponData;

    private Image icon;
    private TextMeshProUGUI text;

    private ItemData Data => weaponData?.itemData ?? new ItemData();

    public void InitItem(WeaponData data)
    {
        weaponData = data;
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = Data.itemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        if (weaponData.Count != 0)
        {
            text.text = $"Lv.{weaponData.CurrentLevel}";
        }
    }

    public void OnClick()
    {
        switch (Data.itemType)
        {
            case ItemData.E_ItemType.Melee:
                HandleWeaponUpgrade();
                EquipHand(true);
                break;
            case ItemData.E_ItemType.Range:
                HandleWeaponUpgrade();
                EquipHand(false);
                var getPlayer = PlayerManager.Instance.GetPlayer();
                getPlayer.Scan.IsWeaponActive = true;
                break;
            case ItemData.E_ItemType.Glove:
                break;
            case ItemData.E_ItemType.Shoe:
                break;
            case ItemData.E_ItemType.Heal:
                break;
        }

        if (weaponData.CurrentLevel >= Data.levelDamage.Length)
        {
            weaponData.SetLevel(Data.levelDamage.Length);
            GetComponent<Button>().interactable = false;
            return;
        }
    }

    private void HandleWeaponUpgrade()
    {
        if (weaponData.Count == 0)
        {
            weaponData.CreateWeapon();
        }
        else
        {
            weaponData.AddLevel(1);
        }
    }

    private void EquipHand(bool isLeft)
    {
        var hand = PlayerManager.Instance.GetPlayer().Hands;
        var equipHand = hand.FirstOrDefault(hand => hand.IsLeft == isLeft);

        if (equipHand != null)
        {
            equipHand.Spriter.sprite = Data.hand;
            equipHand.gameObject.SetActive(true);
        }
    }
}
