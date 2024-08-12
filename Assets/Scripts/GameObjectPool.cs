using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool : MonoBehaviour
{
    [Tooltip("If true, the pool will check if the object is already in the pool before instantiating a new one. Set to false to save CPU cycles.")]
    [SerializeField] private bool collectionCheck;
    [SerializeField] private PooledObject objectToPool;
    [SerializeField] private int defaultCapacity;
    [Tooltip("If the pool is at max capacity, the instantiated objects will be destroyed rather than returned to the pool.")]
    [SerializeField] private int maxCapacity;
    [SerializeField] private bool setActiveOnGet = true;
    
    public ObjectPool<PooledObject> objectPool;

    private void Start()
    {
        objectPool = new (InstantiatePooledObject, OnGetFromPool, OnReturnToPool, OnDestroyPoolObject, collectionCheck, defaultCapacity, maxCapacity);
    }
    
    private PooledObject InstantiatePooledObject()
    {
        GameObject tmp = Instantiate(objectToPool.gameObject);
        PooledObject pooledObject = tmp.GetComponent<PooledObject>();
        pooledObject.SetPool(objectPool);

        return pooledObject;
    }
    
    private void OnGetFromPool(PooledObject pooledObject)
    {
        if (setActiveOnGet)
            pooledObject.gameObject.SetActive(true);

        pooledObject.OnGet();
    }
    
    private void OnReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    /// <summary>
    ///  What will happen when the pool object is destroyed because the pool is at max capacity
    /// </summary>
    /// <param name="pooledObject"></param>
    private void OnDestroyPoolObject(PooledObject pooledObject)
    {
        
    }
}
