using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _xLookSensitivity;
    [SerializeField] private float _yLookSensitivity;
    [SerializeField] private Transform _playerOrientation;

    private float _xRotation;
    private float _yRotation;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _xLookSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _yLookSensitivity;

        this._yRotation += mouseX;

        this._xRotation -= mouseY;
        this._xRotation = Mathf.Clamp(this._xRotation, -90.0f, 90.0f);

        this.transform.rotation = Quaternion.Euler(this._xRotation, this._yRotation, 0);
        this._playerOrientation.rotation = Quaternion.Euler(0, this._yRotation, 0);
    }
}
