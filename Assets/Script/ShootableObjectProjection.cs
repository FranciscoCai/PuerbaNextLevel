using UnityEngine;

public class ShootableObjectProjection : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        ObjectManager.instance.OnObjectIndexChanged += ChangeProjection;
        ChangeProjection(); // Actualiza al habilitar por si ya hay uno seleccionado
    }
    private void OnDisable()
    {
        ObjectManager.instance.OnObjectIndexChanged -= ChangeProjection;
    }

    private void ChangeProjection()
    {
        var shootable = ObjectManager.instance.GetShootableObject();
        if (shootable == null)
        {
            // Limpia el mesh y los materiales si no hay objeto seleccionado
            if (meshFilter != null)
                meshFilter.mesh = null;
            if (meshRenderer != null)
                meshRenderer.sharedMaterials = new Material[0];
            return;
        }

        var sourceMeshFilter = shootable.GetComponent<MeshFilter>();
        var sourceMeshRenderer = shootable.GetComponent<MeshRenderer>();

        if (meshFilter != null && sourceMeshFilter != null)
            meshFilter.mesh = sourceMeshFilter.sharedMesh;

        if (meshRenderer != null && sourceMeshRenderer != null)
            meshRenderer.sharedMaterials = sourceMeshRenderer.sharedMaterials;
    }
}
