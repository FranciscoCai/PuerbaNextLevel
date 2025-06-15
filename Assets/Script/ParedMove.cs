using UnityEngine;

public class ParedMove : MonoBehaviour
{
    public Rigidbody rbA;

    public float velocidad = 5f;

    private void Start()
    {
        // Aseg¨²rate de que el Rigidbody est¨¦ configurado para no rotar
        rbA.linearVelocity = rbA.transform.forward * velocidad;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rbA.linearVelocity = Vector3.zero; // Detener el movimiento al colisionar
    }
}
