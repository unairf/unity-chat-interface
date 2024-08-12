using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public UnityEvent OnGetEvent;
    public UnityEvent OnBeforeReturn;

    private ObjectPool<PooledObject> pool;
    
    public void OnGet()
    {
        OnGetEvent.Invoke();
    }

    public void Return()
    {
        OnBeforeReturn.Invoke();
        pool.Release(this);
    }
    
    public void SetPool(ObjectPool<PooledObject> _pool)
    {
        pool = _pool;
    }
}
