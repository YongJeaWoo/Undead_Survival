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
    // 인스펙터에 할당 가능한 플레이어 적 리스트 목록
    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    // 이름으로 생성할 플레이어
    private Dictionary<string, Enemy> enemyPrefabDic;

    public void InitEnemy()
    {
        // 플레이어의 위치
        var playerPosition = PlayerManager.Instance.GetPlayer().transform.position;

        // 플레이어의 위치를 중심으로 x와 y가 최대 20 정도의 랜덤한 위치
        var spawnPosition = new Vector3(
            playerPosition.x + Random.Range(-20f, 20f),
            playerPosition.y + Random.Range(-20f, 20f),
            playerPosition.z
        );

        // 카메라의 Camera 컴포넌트
        var camera = CameraManager.Instance.MainCamera();

        // 구한 위치가 카메라의 뷰포트 안에 있는지 확인
        var viewportPoint = camera.WorldToViewportPoint(spawnPosition);
        // 구한 위치가 카메라의 뷰포트 안에 있으면 적을 생성하지 않음
        if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1) return;

        // 적 생성
        var index = Random.Range(0, enemyPrefabs.Count);
        var enemyName = enemyPrefabs[index].key;
        var enemy = ObjectPoolManager.Instance.Create(enemyName, transform);

        // 생성된 적의 위치 설정
        enemy.transform.position = spawnPosition;
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
