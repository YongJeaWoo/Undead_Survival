using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 0;
    public float speed;
    public float centerDistance = 1.5f;

    protected Transform playerTransform;

    public virtual void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
    }
}
