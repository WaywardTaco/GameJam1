using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    [SerializeField] private GameObject _playerRef;

    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject == _playerRef){
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndScene");
        }
    }
}
