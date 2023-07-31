using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 10f;
    private float rotationSpeed = 50f;
    private float centerDistance = 1.5f;
    private float defaultAngle = 0f;
    private float accumulateAngle = 0f;

    private Transform playerTransform;

    public float Damage() => damage;
    public float RotationSpeed() => rotationSpeed;
    public float CenterDistance() => centerDistance;

    private void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
    }

    public void SetDefaultAngle(float _angle)
    {
        defaultAngle = _angle;
    }

    private void Update()
    {
        accumulateAngle += rotationSpeed * Time.deltaTime;
        var angle = -(defaultAngle + rotationSpeed * Time.time);
        var offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * centerDistance;
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(0, 0, angle + 270);
    }

    public void SetCenterDistance(float distance)
    {
        centerDistance = distance;
    }
}
