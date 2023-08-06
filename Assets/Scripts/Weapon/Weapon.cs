using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerManager playerManager;

    public float damage;
    public float speed;
    public float centerDistance = 1.5f;

    protected Transform playerTransform;
    protected WeaponManager weaponManager;

    protected Player player;

    public virtual void OnEnable()
    {
        playerManager = PlayerManager.Instance;
        player = playerManager.GetPlayer();
        playerTransform = playerManager.GetPlayer().transform;
        weaponManager = WeaponManager.Instance;

        SetHand();
    }

    private void SetHand()
    {
        var data = playerManager.GetData();
        Hand hand = player.Hands[(int)data.itemType];
        hand.Spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);
    }
}
