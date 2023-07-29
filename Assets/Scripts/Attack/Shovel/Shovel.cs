using UnityEngine;

public class Shovel : MonoBehaviour
{
    private float damage;

    private int per;

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
