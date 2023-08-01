using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject targetObject = null;

    private Transform activeObject = null;
    private Transform deactiveObject = null;

    private List<GameObject> activePoolList = new List<GameObject>();
    private List<GameObject> deactivePoolList = new List<GameObject>();

    public void Initialize(GameObject target)
    {
        targetObject = target;

        activeObject = new GameObject("Active").transform;
        activeObject.SetParent(transform);

        deactiveObject = new GameObject("Deactive").transform;
        deactiveObject.SetParent(transform);
        deactiveObject.gameObject.SetActive(false);
    }

    public Transform GetActiveObject() => activeObject;
    public Transform GetDeactiveObject() => deactiveObject;

    public GameObject Create(Transform _parent = null)
    {
        _parent ??= activeObject;

        if (deactivePoolList.Count <= 0)
        {
            var instantiate = Instantiate(targetObject, _parent);
            instantiate.name = targetObject.name;
            activePoolList.Add(instantiate);
            return instantiate;
        }

        var obj = deactivePoolList.Last();
        obj.transform.SetParent(_parent);
        deactivePoolList.Remove(obj);
        activePoolList.Add(obj);
        return obj;
    }

    public GameObject Create(Transform _parent, Vector3 _position)
    {
        var obj = Create(_parent);
        obj.transform.position = _position;
        return obj;
    }

    public void Return(GameObject _obj)
    {
        var find = activePoolList.Find(x => x.Equals(_obj));

        if (find != null)
        {
            activePoolList.Remove(_obj);
            deactivePoolList.Add(_obj);
            _obj.transform.SetParent(deactiveObject);
        }
    }

    public void ReturnAll()
    {
        for (var i = activePoolList.Count - 1; i >= 0; --i)
        {
            var obj = activePoolList[i];
            activePoolList.RemoveAt(i);

            deactivePoolList.Add(obj);
            obj.transform.SetParent(deactiveObject);
        }
    }
}
