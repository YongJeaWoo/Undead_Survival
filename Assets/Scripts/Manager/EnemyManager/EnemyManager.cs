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
    // 인스펙터에 할당 가능한 플레이어 리스트 목록
    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    // 이름으로 생성할 플레이어
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
