using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{
    public void OnBackToMain(){
        SceneManager.LoadScene("MainMenu");
    }
}
