using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance = null;

    // Delegado y evento para cambio de ¨ªndice
    public delegate void ObjectIndexChangedHandler();
    public event ObjectIndexChangedHandler OnObjectIndexChanged;

    private List<ShootableObject> objects = new List<ShootableObject>();
    private int shootableObjectIndex = 0;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this instance if one already exists
        }
    }

    // Add a GameObject to the list
    public void AddObject(ShootableObject miGameObject)
    {
        objects.Add(miGameObject);
        ChangeObjectIndex(0);
    }

    // Remove a GameObject from the list
    public void RemoveObject(ShootableObject miGameObject)
    {
        objects.Remove(miGameObject);
    }

    // Clear the list
    public void ClearObjects()
    {
        objects.Clear();
        ChangeObjectIndex(0);
    }

    public List<ShootableObject> ReturnObjectList()
    {
        return objects;
    }

    public void ChangeObjectIndex(int changeNumber)
    {
        shootableObjectIndex += changeNumber;
        if (shootableObjectIndex >= objects.Count)
        {
            shootableObjectIndex = 0; // Resetea el ¨ªndice si se pasa del l¨ªmite
        }
        else if (shootableObjectIndex < 0)
        {
            shootableObjectIndex = objects.Count - 1; // Resetea al ¨²ltimo ¨ªndice si es negativo
        }
        OnObjectIndexChanged?.Invoke();
    }

    public ShootableObject GetShootableObject()
    {
        if (objects.Count > 0)
        {
            return objects[shootableObjectIndex];
        }
        return null; // Retorna null si no hay objetos
    }
    public void ActivateAllObjects()
    {
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.gameObject.SetActive(true);
        }
    }
}
