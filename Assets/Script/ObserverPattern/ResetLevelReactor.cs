using UnityEngine;

public class ResetLevelReactor : MonoBehaviour
{
    private Vector3 initialTransformPosition;
    private Quaternion initialTransformRotation;
    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener<int>("Reset", ResetTransform);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<int>("Reset", ResetTransform);
    }
    private void Start()
    {
        initialTransformPosition = gameObject.transform.position;
        initialTransformRotation = gameObject.transform.rotation;
    }
    public void ResetTransform(int num)
    {
        transform.position = initialTransformPosition;
        transform.rotation = initialTransformRotation;
        gameObject.SetActive(true);
    }
}
