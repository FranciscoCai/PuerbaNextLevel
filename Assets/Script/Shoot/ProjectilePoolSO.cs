using UnityEngine;

[CreateAssetMenu(fileName = "ProyectilePoolSO", menuName = "ProyectilePool/ProyectilePoolSO")]
public class ProjectilePoolSO : ComponentProjectileSO<Projectile>
{
    [SerializeField] private ProjectileFactorySO factory;
    public override IFactory<Projectile> Factory { get => factory; set => factory = value as ProjectileFactorySO; }
}
