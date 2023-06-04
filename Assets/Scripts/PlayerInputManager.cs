using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    
    // Player 
    private Material _material;
    private float _rotateDirection;
    [SerializeField]
    private float _rotationSpeed;

    // Driving
    [SerializeField]
    private float _drivingSpeed;
    private Vector3 _drivingDirection;
    private bool _isDriving = false;

    void Start()
    {
        // Input Actions Setup
        _inputActions = new PlayerInputActions();
        if(_inputActions == null )
        {
            Debug.LogWarning("Input Actions are Null!");
        }
        else
        {
            _inputActions.Player.Enable();
        }

        // Player Map Inputs
        _inputActions.Player.ChangeColor.performed += ChangeColor_performed;
        _inputActions.Player.SwapToDriving.performed += SwapToDriving_performed;

        // Driving Map Inputs
        _inputActions.Driving.SwaptoPlayer.performed += SwaptoPlayer_performed;

        // Material Setup
        _material = GetComponent<MeshRenderer>().material;
        if(_material == null )
        {
            Debug.LogWarning("Material is Null!");
        }
    }

    private void Update()
    {
        if(_isDriving) // Check active Map 
        {
            Drive();
        }
        else
        {
            RotatePlayer();
        }
    }

    // Player Actions
    private void SwapToDriving_performed(InputAction.CallbackContext obj)
    {
        _inputActions.Player.Disable();
        _inputActions.Driving.Enable();
        _isDriving = true;
    }
   
    private void ChangeColor_performed(InputAction.CallbackContext obj)
    {
        _material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private void RotatePlayer()
    {
        _rotateDirection = _inputActions.Player.Rotate.ReadValue<float>();
        this.transform.Rotate(new Vector3(0, (_rotateDirection * _rotationSpeed * Time.deltaTime), 0), Space.World);
    }

    // Driving Actions
    private void SwaptoPlayer_performed(InputAction.CallbackContext obj)
    {
        _inputActions.Player.Enable();
        _inputActions.Driving.Disable();
        _isDriving = false;
    }

    private void Drive()
    {
        _drivingDirection.x = _inputActions.Driving.Steer.ReadValue<Vector2>().x;
        _drivingDirection.z = _inputActions.Driving.Steer.ReadValue<Vector2>().y; // Assign Y input to Z movement

        transform.Translate(_drivingDirection * _drivingSpeed * Time.deltaTime);
    }
}
