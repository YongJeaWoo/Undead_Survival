using UnityEngine;

public class RotateWeapon : Weapon
{
    private float defaultAngle = 0f;
    private float accumulateAngle = 0f;

    public float Damage() => damage;
    public float RotationSpeed() => speed;
    public float CenterDistance() => centerDistance;

    public override void OnEnable()
    {
        base.OnEnable();
        damage = 20f;
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

    public void SetDefaultAngle(float _angle)
    {
        defaultAngle = _angle;
    }

    public void SetCenterDistance(float distance)
    {
        centerDistance = distance;
    }
}
