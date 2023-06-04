using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AndroidJoystickInputManager : MonoBehaviour
{
    private AndroidJoystick _inputActions;

    private Vector2 _movementDirection;
    [SerializeField]
    private float _movementSpeed;

    void Start()
    {
        _inputActions = new AndroidJoystick();
        if(_inputActions == null)
        {
            Debug.LogWarning("Input Actions Is Null!");
        }
        else
        {
            _inputActions.Movement.Enable();
        }
    }


    void Update()
    {
        _movementDirection = _inputActions.Movement.Move.ReadValue<Vector2>();
        transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);
    }
}
