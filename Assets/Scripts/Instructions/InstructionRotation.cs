using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InstructionRotation : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - player.transform.position);
    }
}
