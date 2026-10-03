using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmoothCameraBehaviour : MonoBehaviour
{
    [Header("<color=cyan>Camera</color>")]
    [Range(10.0f, 120.0f)] [SerializeField] private float _baseFOV = 75.0f;
    [Range(1.0f, 1.5f)] [SerializeField] private float _moveFOVModifier = 1.125f;
    [Range(0.0f, 1.0f)] [SerializeField] private float _maxDistance = 0.125f;
    [Range(0.01f, 0.125f)] [SerializeField] private float _smoothSpeed = 0.075f;

    private float _moveFOV = 0.0f;
    
    private BasePlayerModel _playerModel;
    private Camera _camera;

    private Vector3 _offset = new(), _desiredPos = new(), _expandedPos = new(), _smoothedPos = new();

    private void Start()
    {
        _playerModel = GameManager.Instance.BasePlayer;

        _camera = Camera.main;
        
        _offset = transform.position - _playerModel.transform.position;

        _moveFOV = _baseFOV * _moveFOVModifier;
    }

    private void FixedUpdate()
    {
        _desiredPos = _playerModel.transform.position + _offset;

        _expandedPos = transform.position + _playerModel.MoveDir * _maxDistance;

        _smoothedPos = Vector3.Lerp(_expandedPos, _desiredPos, _smoothSpeed);
        
        transform.position = _smoothedPos;

        _camera.fieldOfView = Mathf.Lerp(_baseFOV, _moveFOV, _playerModel.MoveDir.magnitude);
    }
}
