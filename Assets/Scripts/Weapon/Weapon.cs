using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 10f;
    private float rotationSpeed = 2f;
    private float centerDistance = 1.5f;

    private Transform playerTransform;

    public float Damage() => damage;
    public float RotationSpeed() => rotationSpeed;
    public float CenterDistance() => centerDistance;

    private void OnEnable()
    {
        playerTransform = PlayerManager.Instance.GetPlayer().transform;
    }

    private void Update()
    {
        var angle = rotationSpeed * Time.time;
        var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * centerDistance;
        transform.position = playerTransform.position + offset;
    }
}
