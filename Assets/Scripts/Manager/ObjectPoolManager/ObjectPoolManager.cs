using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonComponent<ObjectPoolManager>
{
    // ������Ʈ Ǯ ��ų ������Ʈ�� �ִ� �� 
    [SerializeField] private List<GameObject> poolObjects = null;

    private Dictionary<string, ObjectPool> objectPoolDics = new Dictionary<string, ObjectPool>();

    // ������Ʈ Ǯ�� ã�Ƽ� ���
    private void Initialized()
    {
        poolObjects.ForEach(CreateObjectPool);
    }

    private void CreateObjectPool(GameObject _obj)
    {
        var obj = new GameObject($"{_obj.name}_Pool");
        obj.transform.SetParent(transform);
        var pool = obj.AddComponent<ObjectPool>();
        objectPoolDics.Add(_obj.name, pool);
        pool.Initialize(_obj);
    }

    public ObjectPool GetPool(string _poolName)
    {
        return objectPoolDics.TryGetValue(_poolName, out var result) ? result : null;
    }

    // �� ������Ʈ�� ����
    public Enemy CreateEnemy(string _poolName, Transform _parent = null)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}�� ���� ������Ʈ Ǯ�� �������� ����");
            return null;
        }

        return pool.Create(_parent).GetComponent<Enemy>();
    }

    public GameObject Create(string _poolName)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}�� ���� ������Ʈ Ǯ�� �������� ����");
            return null;
        }

        return pool.Create();
    }

    // ������ ���� �Ͽ� ����
    public GameObject Create(string _poolName, Transform _parent)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}�� ���� ������Ʈ Ǯ�� �������� ����");
            return null;
        }

        return pool.Create(_parent);
    }
    
    public void Return(GameObject _obj, Transform _parent = null)
    {
        var pool = GetPool(_obj.name);

        if (pool == null)
        {
            Debug.LogError($"{_obj.name}�� ���� ������Ʈ Ǯ�� �������� ����");
            return;
        }

        pool.Return(_obj);
    }

    public void ReturnAll(string _poolName)
    {
        var pool = GetPool(_poolName);

        if (pool == null)
        {
            Debug.LogError($"{_poolName}�� ���� ������Ʈ Ǯ�� �������� ����");
            return;
        }

        pool.ReturnAll();
    }

    #region Singleton

    protected override void AwakeInstance()
    {
        Initialized();
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
