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
        text.text = $"Lv.{level + 1}";
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.E_ItemType.Melee:
                {

                }
                break;
            case ItemData.E_ItemType.Range:
                {

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
