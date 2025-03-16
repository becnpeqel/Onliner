using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Vector3 _offsets = new Vector3(0, 5, -4);
    private Transform _target;
    private Vector3 _velocity;
    [SerializeField] private float _smooth = 0.5f;
    [SerializeField] private float _forwardOffsetMultiplayer = 2.0f;
    public void SetTarger(Transform target)
    {
        _target = target;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_target == null)
        {
            return;
        }
        Vector3 newPos = Vector3.SmoothDamp(transform.position, _target.position + _offsets, ref _velocity, _smooth);
        transform.position = newPos;
    }
}