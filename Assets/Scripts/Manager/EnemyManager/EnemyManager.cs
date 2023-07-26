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

    public void InitEnemy()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            var index = Random.Range(0, enemyPrefabs.Count);

            var enemyName = enemyPrefabs[index].key;

            ObjectPoolManager.Instance.Create(enemyName, transform);
        }
    }

    private void SetEnemies()
    {
        enemyPrefabDic = new Dictionary<string, Enemy>();

        foreach (var enemyPrefab in enemyPrefabs)
        {
            enemyPrefabDic.Add(enemyPrefab.key, enemyPrefab.enemy);
        }
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
