using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private ProgressBarInputActions _inputActions;
    [SerializeField]
    private Scrollbar _progressBar;
    [SerializeField]
    private TMP_Text _progressText;
    [SerializeField]
    private float _chargeSpeed; // Percentage per second.  0.25 = 25%

    private bool _isCharging = false;
    private float _chargeValue = 0f;

    void Start()
    {
        _inputActions = new ProgressBarInputActions();
        if(_inputActions == null)
        {
            Debug.LogWarning("Input Actions is Null!");
        }
        else
        {
            _inputActions.ProgressBar.Enable();
        }

        _inputActions.ProgressBar.Charge.started += Charge_started;
        _inputActions.ProgressBar.Charge.canceled += Charge_canceled;
    }

    private void Charge_started(InputAction.CallbackContext obj)
    {
        _isCharging = true; // Space bar pressed
    }

    private void Charge_canceled(InputAction.CallbackContext obj)
    {
        _isCharging = false; // Space bar released
    }


    void Update()
    {
        if(_isCharging)
        {
            Charge();
        }
        else
        {
            Discharge();
        }

        UpdateProgressBar();
    }

    private void Charge()
    {
        _chargeValue += _chargeSpeed * Time.deltaTime;
        if (_chargeValue > 1) // Limit upper value to 1
        {
            _chargeValue = 1;
        }
    }

    private void Discharge()
    {
        _chargeValue -= _chargeSpeed * Time.deltaTime;
        if (_chargeValue < 0) // Limit lower value to 0
        {
            _chargeValue = 0;
        }
    }

    private void UpdateProgressBar()
    {
        _progressBar.size = _chargeValue;
        _progressText.text = (_chargeValue * 100).ToString("0.0") + "%";
    }
}
