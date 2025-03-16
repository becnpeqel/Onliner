using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{
    public bool isTarget { get; set; } = false;
    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    //Это Ванюша если что.
    private void Update()
    {
        isTarget = false;
    }
    private void LateUpdate()
    {
        _meshRenderer.enabled = isTarget;
    }
}