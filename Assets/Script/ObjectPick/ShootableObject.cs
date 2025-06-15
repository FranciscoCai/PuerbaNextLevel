using UnityEngine;

public class ShootableObject : MonoBehaviour
{

    public void HasBeenShot()
    {
        if (ObjectManager.instance != null)
        {
            // Remove this GameObject from the ObjectManager's list  
            ObjectManager.instance.AddObject(this);
        }
        else
        {
        }
    }
}
