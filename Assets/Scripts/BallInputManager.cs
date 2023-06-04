using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallInputManager : MonoBehaviour
{
    private BouncingBallInputActions _inputActions;

    [SerializeField]
    private float _bouncePower;

    private float _velocity;
    private bool _isMoving = false;
    private float _currentBouncePower;
    private Rigidbody _rigidBody;

    void Start()
    {
        // Input Actions
        _inputActions = new BouncingBallInputActions();
        if(_inputActions == null)
        {
            Debug.LogWarning("Input Actions is Null!");
        }
        else
        {
            _inputActions.BouncingBall.Enable();
        }

        _inputActions.BouncingBall.Bounce.performed += Bounce_performed;
        _inputActions.BouncingBall.Bounce.canceled += Bounce_canceled;

        // Rigid Body
        _rigidBody = GetComponent<Rigidbody>();
        if(_rigidBody == null)
        {
            Debug.Log("Rigidbody in Null");
        }
    }

    private void Update()
    {
        CalculateVelocity();
    }

    private void Bounce_canceled(InputAction.CallbackContext obj)
    {
        if(!_isMoving)
        {
            _currentBouncePower = _bouncePower * (float)obj.duration;
            BounceBall();
        }
    }

    private void Bounce_performed(InputAction.CallbackContext obj)
    {
        if (!_isMoving)
        {
            _currentBouncePower = _bouncePower;
            BounceBall();
        }
    }

    private void BounceBall()
    {
        _rigidBody.velocity = Vector3.up * _currentBouncePower;
    }

    private void CalculateVelocity()
    {
        _velocity = _rigidBody.velocity.magnitude;
        if(_velocity == 0)
        {
            _isMoving = false;
        }
        else 
        {
            _isMoving = true;
        }
    }
}
