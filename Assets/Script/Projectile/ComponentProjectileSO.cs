using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentProjectileSO<T> : ScriptableObject where T : Component
{
    public int initialPoolSize = 10;

    private Queue<T> pool = new Queue<T>();

    public abstract IFactory<T> Factory { get; set; }
    public void Prewarm(int InitialSize)
    {
        initialPoolSize = InitialSize;
        InitializePool();
    }
    public void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = Factory.Create();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T GetFromPool()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = Factory.Create();
            return obj;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
