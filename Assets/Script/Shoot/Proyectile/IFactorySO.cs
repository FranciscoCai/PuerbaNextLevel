using UnityEngine;

public abstract class IFactorySO<T> : ScriptableObject, IFactory<T>
{
    public abstract T Create();
}
