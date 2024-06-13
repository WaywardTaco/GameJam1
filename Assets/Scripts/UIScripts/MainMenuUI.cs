using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartPlay(){
        SceneManager.LoadScene("GameScene");
    }

    public void OnClose(){
        Application.Quit();
    }
}
