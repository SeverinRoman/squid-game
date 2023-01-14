//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
//#endregion


public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    private class PoolConfig
    {
        [SerializeField] public GameObjectType type;
        [SerializeField] public GameObject gameObject;

        [SerializeField] public bool isPrewarm = false;
        [SerializeField] public bool isCollectionChecks = false;
        [SerializeField] public int defaultCapacity = 10;
        [SerializeField] public int maxPoolSize = 10;

        public ObjectPool<GameObject> pool;
    }

    //#region editors fields and properties
    [SerializeField] private List<PoolConfig> poolConfigs;

    [Space]
    [Header("Debug Setttings")]
    [SerializeField] private bool isDebug;
    [SerializeField] private GameObjectType debugGameObjectType;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private ObjectPool<GameObject> _pool;

    //#endregion


    //#region life-cycle callbacks

    private void Awake()
    {
        Init();
    }

    void OnEnable()
    {
        GameEventManager.GetPoolObject.AddListener(OnGetPoolObject);
    }

    void OnDisable()
    {
        GameEventManager.GetPoolObject.RemoveListener(OnGetPoolObject);
    }

    //#endregion

    //#region public methods

    public void ReturnPoolObject(GameObjectType type, GameObject gameObject)
    {
        foreach (PoolConfig poolConfig in poolConfigs)
        {
            if (type == poolConfig.type)
            {
                poolConfig.pool.Release(gameObject);
            }
        }
    }

    //#endregion

    //#region private methods

    private void Init()
    {
        foreach (PoolConfig poolConfig in poolConfigs)
        {
            CreatePool(poolConfig);

            if (poolConfig.isPrewarm)
            {
                PrewarmPool(poolConfig);
            }
        }
    }

    private void PrewarmPool(PoolConfig poolConfig)
    {
        List<GameObject> prewarmObjects = new List<GameObject>();
        for (int i = 0; i < poolConfig.maxPoolSize; i++)
        {
            GameObject gameObjectPool = poolConfig.pool.Get();
            gameObjectPool.transform.parent = transform;
            prewarmObjects.Add(gameObjectPool);

        }

        foreach (GameObject gameObject in prewarmObjects)
        {
            poolConfig.pool.Release(gameObject);
        }
    }

    private void CreatePool(PoolConfig poolConfig)
    {
        GameObject CreateFunc()
        {
            if (!poolConfig.gameObject.TryGetComponent<PoolObject>(out PoolObject poolObject))
            {
                PoolObject newPoolObject = poolConfig.gameObject.AddComponent<PoolObject>();
                newPoolObject.SetPoolConfig(poolConfig.type, this);
            }
            else
            {
                poolObject.SetPoolConfig(poolConfig.type, this);
            }

            return Instantiate(poolConfig.gameObject, Vector3.zero, Quaternion.identity);
        }

        void ActionOnGet(GameObject gameObject)
        {


            gameObject.SetActive(true);
        }

        void ActionOnRelease(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(transform, true);
            gameObject.transform.localPosition = Vector3.zero;
        }

        void ActionOnDestroy(GameObject gameObject)
        {
            Destroy(gameObject);
        }

        poolConfig.pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, poolConfig.isCollectionChecks, poolConfig.defaultCapacity, poolConfig.maxPoolSize);
    }

    private void ClearPool()
    {
        foreach (PoolConfig poolConfig in poolConfigs)
        {
            poolConfig.pool.Dispose();
        }
    }

    private GameObject GetPoolObject(GameObjectType type)
    {
        foreach (PoolConfig poolConfig in poolConfigs)
        {
            if (type == poolConfig.type)
            {
                GameObject poolGameObject = poolConfig.pool.Get();
                poolGameObject.transform.parent = null;
                return poolGameObject;
            }

        }
        return null;
    }

    private void OnGUI()
    {
        if (isDebug)
        {
            foreach (PoolConfig poolConfig in poolConfigs)
            {
                if (debugGameObjectType == poolConfig.type)
                {
                    GUI.Label(new Rect(10, 10, 200, 30), $"Total Pool Size: {poolConfig.pool.CountAll}");
                    GUI.Label(new Rect(10, 30, 200, 30), $"Active Objects: {poolConfig.pool.CountActive}");
                    break;
                }
            }
        }
    }

    [NaughtyAttributes.Button]
    private void SpawnPoolObject()
    {
        GetPoolObject(debugGameObjectType);
    }

    //#endregion

    //#region event handlers

    protected void OnGetPoolObject(GameObjectType type, Action<GameObject> callback)
    {
        callback(GetPoolObject(type));
    }

    //#endregion

}
