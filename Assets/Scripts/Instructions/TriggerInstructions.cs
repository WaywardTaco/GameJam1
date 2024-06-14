using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerInstructions : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMeshPro1;
    [SerializeField] TextMeshPro _textMeshPro2;

    private void Start()
    {
        _textMeshPro2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _textMeshPro1.gameObject.SetActive(false);
        _textMeshPro2.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && _textMeshPro2.gameObject.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }
}
