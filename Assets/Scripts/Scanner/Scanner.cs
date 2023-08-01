using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private float scanRange = 5f;
    private LayerMask enemyLayer;

    private Transform playerTransform;
    private Transform target;

    #region Property

    public Vector3 Target => target != null ? target.position : Vector3.zero;

    #endregion

    private void OnEnable()
    {
        playerTransform = transform;
    }

    private void Update()
    {
        ScanForEnemies();
    }

    private void ScanForEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, scanRange, enemyLayer);

        if (colliders.Length > 0)
        {
            target = colliders.OrderBy(c => Vector3.Distance(playerTransform.position, c.transform.position)).First().transform;
        }
        else
        {
            target = null;
        }
    }
}
