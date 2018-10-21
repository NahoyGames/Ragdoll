using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeTracks : MonoBehaviour {

    SkinnedMeshRenderer meshRenderer;
    public MeshCollider col;

    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        col.sharedMesh = null;
        col.sharedMesh = colliderMesh;
    }
}
