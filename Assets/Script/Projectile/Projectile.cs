using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectilePoolSO projectilePool;
    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
        CancelInvoke(nameof(DestroyProjectile));
        Invoke(nameof(DestroyProjectile), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DestroyProjectile));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<ShootableObject>(out ShootableObject shootableObject))
        {
            shootableObject.gameObject.SetActive(false);
            shootableObject.HasBeenShot(); 
        }
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        if (!isActive) return; // Evita devolverlo dos veces
        isActive = false;

        if (projectilePool != null)
        {
            projectilePool.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject); // Fallback
        }
    }
}
