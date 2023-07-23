using System.Collections.Generic;
using UnityEngine;


namespace SingletonComponent
{
    public static class AbleInstance
    {
        public static bool isAble = true;
    }

    namespace Data
    {
        public abstract class SingletonData
        {
            public static readonly List<SingletonData> singletonList = new List<SingletonData>();

            protected static void PushSingleton(SingletonData _obj)
            {
                if (null != _obj)
                {
                    singletonList.Add(_obj);

                    _obj.Initialize();
                }
            }

            public static bool InitSingletons()
            {
                int count = singletonList.Count;
                for (int n = 0; n < count; ++n)
                {
                    if (null != singletonList[n])
                    {
                        if (false == singletonList[n].Initialize())
                            return false;
                    }
                }

                return true;
            }

            public static void ReleaseSingletons()
            {
                int count = singletonList.Count;
                for (int n = 0; n < count; ++n)
                {
                    if (null != singletonList[n])
                    {
                        singletonList[n].ReleaseSingleton();
                    }
                }
                singletonList.Clear();
            }

            public abstract bool Initialize();
            protected abstract void ReleaseSingleton();
        }

        public abstract class SingletonComponent<T> : SingletonData where T : class, new()
        {
            private static T m_instance = null;
            public static T Instance
            {
                get
                {
                    if (null == m_instance)
                    {
                        m_instance = new T();
                        SingletonData.PushSingleton(m_instance as SingletonData);
                    }
                    return m_instance;
                }
            }

            private bool _initialized = false;

            public bool IsInitialized { get => _initialized; }

            public override bool Initialize()
            {
                if (!_initialized)
                {
                    _initialized = InitInstance();
                    return _initialized;
                }

                return true;
            }

            protected override void ReleaseSingleton()
            {
                _initialized = false;
                ReleaseInstance();

                m_instance = null;
            }

            protected abstract bool InitInstance();
            protected abstract void ReleaseInstance();
        }
    }

    namespace Component
    {
        public abstract class SingletonComponent : MonoBehaviour
        {
            protected static bool _isProcessing_Release = false;
            public static readonly List<SingletonComponent> singletonList = new List<SingletonComponent>();

            private static GameObject DontDestroyParent = null;

            protected static void PushSingleton(SingletonComponent _obj)
            {
                if (null != _obj)
                {
                    InitInstanceDontDestroyParent();

                    _obj.transform.SetParent(DontDestroyParent.transform);

                    singletonList.Add(_obj);
                    _obj.AwakeSingleton();
                }
            }

            public static bool InitSingletons()
            {
                int count = singletonList.Count;
                for (int n = 0; n < count; ++n)
                {
                    if (null != singletonList[n])
                    {
                        if (false == singletonList[n].Initialize())
                            return false;
                    }
                }

                return true;
            }

            public static void ReleaseSingletons()
            {
                if (!_isProcessing_Release)
                {
                    _isProcessing_Release = true;

                    int count = singletonList.Count;

                    //�ڷ�ƾ ����
                    for (int n = count - 1; n >= 0; --n)
                    {
                        if (null != singletonList[n])
                            singletonList[n].StopAllCoroutines();
                    }

                    //������ ������Ʈ���� �������� �Ŵ����� �����ϸ� �ٽ� �����Ǳ� ������
                    //��� ��Ȱ��ȭ ��Ų �� ���� ��Ű���� �Ѵ�
                    for (int n = count - 1; n >= 0; --n)
                    {
                        SingletonComponent _component = singletonList[n];
                        if (null != _component && _component.gameObject.activeInHierarchy)
                            _component.gameObject.SetActive(false);
                    }

                    //��Ȱ��ȭ�� �Ϸ�Ǹ� ��� ����
                    for (int n = count - 1; n >= 0; --n)
                    {
                        if (null != singletonList[n])
                        {
                            singletonList[n].ReleaseSingleton();
                        }
                    }

                    singletonList.Clear();

                    RenewDontDestroyParent();

                    _isProcessing_Release = false;
                }
            }

            protected abstract void AwakeSingleton();
            public abstract bool Initialize();
            protected abstract void ReleaseSingleton();

            private static void InitInstanceDontDestroyParent()
            {
                if (DontDestroyParent == null)
                {
                    DontDestroyParent = new GameObject("Singletons");
                    GameObject.DontDestroyOnLoad(DontDestroyParent);
                }
            }

            private static void RenewDontDestroyParent()
            {
                if (DontDestroyParent != null)
                {
                    GameObject.Destroy(DontDestroyParent);
                    DontDestroyParent = null;
                    InitInstanceDontDestroyParent();
                }
            }
        }

        public abstract class SingletonComponent<T> : SingletonComponent where T : MonoBehaviour
        {
            private static T m_instance;

            public static bool IsQuitting()
            {
                return (!AbleInstance.isAble || _isProcessing_Release);
            }

            public static T Instance
            {
                get
                {
                    if (IsQuitting())
                    {
                        throw new System.Exception("Instance(" + typeof(T) + ") already destroyed on application quit.");
                    }

                    if (m_instance == null)
                    {
                        T _instance = FindObjectOfType(typeof(T)) as T;
                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            singleton.name = "(Singleton)" + typeof(T).ToString();
                            m_instance = singleton.AddComponent<T>();
                        }
                        else
                        {
                            m_instance = _instance;
                            PushSingleton(m_instance as SingletonComponent);
                        }
                    }

                    return m_instance;
                }
            }

            public bool isInitialized { get { return _initialized; } }
            private bool _initialized = false;

            public override bool Initialize()
            {
                if (!_initialized)
                {
                    _initialized = true;
                    return InitInstance();
                }

                return true;
            }

            protected override void AwakeSingleton()
            {
                AwakeInstance();
            }

            protected override void ReleaseSingleton()
            {
                _initialized = false;

                if (null != m_instance)
                {
                    ReleaseInstance();
                    Destroy(m_instance);
                }

                m_instance = null;
            }

            protected abstract void AwakeInstance();
            protected abstract bool InitInstance();
            protected abstract void ReleaseInstance();


            void Awake()
            {
                useGUILayout = false;

                if (null == m_instance)
                {
                    m_instance = this as T;
                    PushSingleton(m_instance as SingletonComponent);
                }
            }

            void OnApplicationQuit()
            {
                if (_initialized)
                {
                    //����ǰ� �������� ReleaseSingleton()�� ȣ������ ����
                    ReleaseInstance();
                }

                m_instance = null;
            }
        }
    }
}