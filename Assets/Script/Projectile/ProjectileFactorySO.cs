using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFactory", menuName = "Factory/ProjectileFactory")]
public class ProjectileFactorySO : IFactorySO<Projectile>
{
    public Projectile proyectilePrefab;
    public override Projectile Create()
    {
        // Implementaci¨®n de creaci¨®n de proyectil
        return Instantiate(proyectilePrefab);
    }
}
