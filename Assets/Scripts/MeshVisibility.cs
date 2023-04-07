

using UnityEngine;

public class MeshVisibility : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;

    void Start()
    {
        // Get the mesh renderer component of the player character and set it invisible
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshRenderer.enabled = false;
    }
}