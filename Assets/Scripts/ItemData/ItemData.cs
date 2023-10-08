using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum E_ItemType
    {
        None,
        Melee,
        Range,
        Glove,
        Shoe,
        Heal
    }

    public E_ItemType itemType;

    [Header("@ Item")]
    public string keyName;
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemObject;
    
    [Header("@ Weapon Hand")]
    public Sprite hand;

    [Header("@ Level Weapon")]
    public float baseDamage;
    public float baseSpeed;

    public float[] levelDamage;
    public float[] levelSpeed;
}
