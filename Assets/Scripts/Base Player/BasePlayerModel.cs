using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasePlayerModel : MonoBehaviour
{
    private BasePlayerController _controller;
    private BasePlayerView _view;
    
    [Header("<color=orange>Inputs</color>")]
    [SerializeField] private float _smoothInputSpeed = 0.2f;

    [Header("<color=orange>Physics</color>")]
    [SerializeField] private float _moveSpeed = 3.5f;
    
    private Rigidbody _rb;

    private Vector2 _inputDir = new(), _smoothInputDir = new(), _smoothInputVelocity = new();
    public Vector2 InputDir
    {
        get { return _inputDir; }
        set { _inputDir = value; }
    }

    private Vector3 _moveDir = new();
    public Vector3 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value; }
    }

    private void Awake()
    {
        GameManager.Instance.BasePlayer = this;

        _controller = GetComponent<BasePlayerController>();
        _view = GetComponentInChildren<BasePlayerView>();
        
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX |  RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        _smoothInputDir = Vector2.SmoothDamp(_smoothInputDir, _inputDir, ref _smoothInputVelocity, _smoothInputSpeed);
        
        _view.UpdateMovementAxis(_smoothInputDir);
    }

    private void FixedUpdate()
    {
        if (_smoothInputDir.x != 0.0f ||  _smoothInputDir.y != 0.0f)
        {
            Movement(_smoothInputDir);
        }
    }

    private void Movement(Vector2 dir)
    {
        _moveDir = transform.right * dir.x + transform.forward * dir.y;
        
        _rb.MovePosition(transform.position + _moveDir * _moveSpeed * Time.fixedDeltaTime);
    }
}
