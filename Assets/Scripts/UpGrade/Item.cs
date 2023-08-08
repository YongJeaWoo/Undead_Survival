using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Weapon weapon;
    public ItemData data;
    public int level;

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
        text.text = $"Lv.{level}";
    }

    public void OnClick()
    {
        if (level == 0)
        {
            var infoObj = ObjectPoolManager.Instance.infoObj;
            var getWeaponManager = WeaponManager.Instance.GetWeapon();
            var getObject = getWeaponManager[data.keyName].itemObject;
            var newObject = new GameObject($"{getObject.name} Info");
            newObject.transform.SetParent(infoObj.transform);

            var weaponType = getObject.GetComponent<Weapon>().GetType();
            weapon = (Weapon)newObject.AddComponent(weaponType);

            if (weapon != null)
            {
                weapon.data = data;
                weapon.Init();
            }
        }
        else
        {
            if (weapon != null)
            {
                weapon.LevelUp(level);
            }
        }

        switch (data.itemType)
        {
            case ItemData.E_ItemType.Melee:
                {
                    var hand = PlayerManager.Instance.GetPlayer().Hands;
                    var leftHand = hand.FirstOrDefault(hand => hand.IsLeft);

                    if (leftHand != null)
                    {
                        leftHand.Spriter.sprite = data.hand;
                        leftHand.gameObject.SetActive(true);
                    }

                    WeaponManager.Instance.AddRotateWeapon("Shovel");
                }
                break;
            case ItemData.E_ItemType.Range:
                {
                    var Player = PlayerManager.Instance.GetPlayer();
                    var hand = PlayerManager.Instance.GetPlayer().Hands;

                    var rightHand = hand.FirstOrDefault(hand => !hand.IsLeft);

                    if (rightHand != null)
                    {
                        rightHand.Spriter.sprite = data.hand;
                        rightHand.gameObject.SetActive(true);
                    }

                    Player.Scan.IsWeaponActive = true;
                }
                break;
            case ItemData.E_ItemType.Glove:
                {

                }
                break;
            case ItemData.E_ItemType.Shoe:
                {

                }
                break;
            case ItemData.E_ItemType.Heal:
                {

                }
                break;
        }

        level++;

        if (level == data.levelDamage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
