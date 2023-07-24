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
    // �ν����Ϳ� �Ҵ� ������ �÷��̾� ����Ʈ ���
    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    // �̸����� ������ �÷��̾�
    private Dictionary<string, Enemy> enemyPrefabDic;

    private List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> GetEnemies() => enemies;

    public void InitEnemy(string name)
    {
        Enemy enemy = Instantiate(enemyPrefabDic[name]);
        enemies.Add(enemy);
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
