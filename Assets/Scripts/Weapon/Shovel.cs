using UnityEngine;

public class Shovel : Weapon
{
    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        accumulateAngle += speed * Time.deltaTime;
        var angle = -(defaultAngle + speed * Time.time);
        var facingAngle = angle + 270;
        var offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * centerDistance;
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(0, 0, facingAngle);
    }

    public override void Init()
    {
        base.Init();
        centerDistance = 1.5f;
    }
}
