using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMovement : MonoBehaviour
{
    void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * 0.5f, Space.World); 
    }
}
