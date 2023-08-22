using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeButton : MonoBehaviour
{
    public Weapon weapon;
    public ItemData data;

    private Image icon;
    private TextMeshProUGUI text;

    private void Awake()
    {
        InitItem();
    }

    private void InitItem()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        if (weapon != null)
        {
            text.text = $"Lv.{weapon.level}";
            
        }
    }

    public void OnClick()
    {
        if (weapon.level == 0)
        {
            var getWeaponManager = WeaponManager.Instance.GetWeapon();
            var getObject = getWeaponManager[data.keyName];
            var weaponObject = data.itemObject;

            if (weaponObject != null)
            {
                weapon = weaponObject.GetComponent<Weapon>();

                if (weapon != null)
                {
                    weapon.data = data;
                    weapon.Init();
                }
            }
        }
        else
        {
            weapon.LevelUp();
        }

        //var player = PlayerManager.Instance.GetPlayer();
        //var bulletObject = player.Scan.BulletObj;

        //if (bulletObject != null)
        //{
        //    var getBullet = bulletObject.GetComponent<Bullet>();

        //    getBullet.damage = data.baseDamage + data.levelDamage[level];
        //    getBullet.speed = data.baseSpeed + data.levelSpeed[level];
        //}

        switch (data.itemType)
        {
            case ItemData.E_ItemType.Melee:
                EquipHand(true);
                WeaponManager.Instance.AddRotateWeapon("Shovel");
                break;
            case ItemData.E_ItemType.Range:
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

        if (weapon.level >= data.levelDamage.Length)
        {
            weapon.level = data.levelDamage.Length;
            GetComponent<Button>().interactable = false;
            return;
        }
    }

    private void EquipHand(bool isLeft)
    {
        var hand = PlayerManager.Instance.GetPlayer().Hands;
        var equipHand = hand.FirstOrDefault(hand => hand.IsLeft == isLeft);

        if (equipHand != null)
        {
            equipHand.Spriter.sprite = data.hand;
            equipHand.gameObject.SetActive(true);
        }
    }
}
