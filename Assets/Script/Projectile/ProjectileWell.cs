using UnityEngine;
using UnityEngine.ProBuilder;

public class ProjectileWell : MonoBehaviour
{
    public ProjectilePoolSO proyectilePool; // Reference to the projectile factory ScriptableObject
    public int initialPoolSize = 10; // Initial size of the projectile pool

    private void Start()
    {
        // Initialize the projectile pool with the specified size
        proyectilePool.Prewarm(initialPoolSize); // Replace Prewarm with InitializePool
    }

    public void SpawnAProyectile(float velocity)
    {
        Projectile projectile = proyectilePool.GetFromPool();
        projectile.projectilePool = proyectilePool; // Set the projectile's pool reference
        projectile.transform.position = gameObject.transform.position; // Spawn the projectile in front of the player
        projectile.transform.rotation = gameObject.transform.rotation; // Set the projectile's rotation to match the player's rotation
        projectile.GetComponent<Rigidbody>().linearVelocity = projectile.transform.forward * velocity;
    }

    public bool SpawnAObject(float velocity, ShootableObject shootableObject)
    {
        Vector3 spawnPosition = gameObject.transform.position;
        Quaternion spawnRotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);

        Collider col = shootableObject.GetComponent<Collider>();
        bool isBlocked = false;

        if (col is BoxCollider box)
        {
            Vector3 worldCenter = spawnPosition + spawnRotation * box.center;
            Vector3 halfExtents = Vector3.Scale(box.size * 0.5f, shootableObject.transform.lossyScale);
            isBlocked = Physics.CheckBox(worldCenter, halfExtents, spawnRotation, ~0, QueryTriggerInteraction.Ignore);
        }
        else if (col is SphereCollider sphere)
        {
            Vector3 worldCenter = spawnPosition + spawnRotation * sphere.center;
            float worldRadius = sphere.radius * Mathf.Max(
                shootableObject.transform.lossyScale.x,
                shootableObject.transform.lossyScale.y,
                shootableObject.transform.lossyScale.z
            );
            isBlocked = Physics.CheckSphere(worldCenter, worldRadius, ~0, QueryTriggerInteraction.Ignore);
        }
        else if (col is MeshCollider meshCol)
        {
            Bounds bounds = meshCol.sharedMesh.bounds;
            Vector3 worldCenter = spawnPosition + spawnRotation * bounds.center;
            Vector3 halfExtents = Vector3.Scale(bounds.extents, shootableObject.transform.lossyScale);
            isBlocked = Physics.CheckBox(worldCenter, halfExtents, spawnRotation, ~0, QueryTriggerInteraction.Ignore);
        }

        if (isBlocked)
        {
            return false;
        }

        shootableObject.gameObject.SetActive(true);
        shootableObject.transform.position = spawnPosition;
        shootableObject.transform.rotation = spawnRotation;
        var rb = shootableObject.GetComponent<Rigidbody>();
        rb.linearVelocity = shootableObject.transform.forward * velocity;
        rb.angularVelocity = Vector3.zero;
        return true;
    }
}
