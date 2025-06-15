using UnityEngine;
using UnityEngine.EventSystems;

public class ResetActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterInputLogic>(out CharacterInputLogic characterController))
        {
            ObjectManager.instance.ActivateAllObjects();
            EventCenter.Instance.EventTrigger<int>("Reset", 0);
            ObjectManager.instance.ClearObjects();
            characterController.SetSpawnPoint(transform.position,transform.rotation);
        }
    }
}
