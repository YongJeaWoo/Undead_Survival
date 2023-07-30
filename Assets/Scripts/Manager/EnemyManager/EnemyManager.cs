using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public string key;
    public Enemy enemy;
}

public class EnemyManager : SingletonComponent<EnemyManager>
{
    // �ν����Ϳ� �Ҵ� ������ �÷��̾� �� ����Ʈ ���
    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    // �̸����� ������ �÷��̾�
    private Dictionary<string, Enemy> enemyPrefabDic;

    private int level = 1;

    private void SetEnemies()
    {
        enemyPrefabDic = new Dictionary<string, Enemy>();

        foreach (var enemyPrefab in enemyPrefabs)
        {
            enemyPrefabDic.Add(enemyPrefab.key, enemyPrefab.enemy);
        }
    }

    public void InitEnemy()
    {
        // �÷��̾��� ��ġ
        var playerPosition = PlayerManager.Instance.GetPlayer().transform.position;

        // �÷��̾��� ��ġ�� �߽����� x�� y�� �ִ� 20 ������ ������ ��ġ
        var spawnPosition = new Vector3(
            playerPosition.x + Random.Range(-20f, 20f),
            playerPosition.y + Random.Range(-20f, 20f),
            playerPosition.z
        );

        // ī�޶��� Camera ������Ʈ
        var camera = CameraManager.Instance.MainCamera();

        // ���� ��ġ�� ī�޶��� ����Ʈ �ȿ� �ִ��� Ȯ��
        var viewportPoint = camera.WorldToViewportPoint(spawnPosition);
        // ���� ��ġ�� ī�޶��� ����Ʈ �ȿ� ������ ���� �������� ����
        if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1) return;

        // �� ����
        var index = Random.Range(0, enemyPrefabs.Count);
        var enemyName = enemyPrefabs[index].key;
        var enemy = ObjectPoolManager.Instance.CreateEnemy(enemyName, transform);

        enemy.SetHealth(100 * level);

        // ������ ���� ��ġ ����
        enemy.transform.position = spawnPosition;
    }

    #region Singleton

    protected override void AwakeInstance()
    {
        SetEnemies();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion
}
