using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    
    private void Update(){
        this.transform.position = this._cameraPosition.position;
    }
}
