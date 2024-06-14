using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private AudioClip _clickSound;
    private AudioSource _audioPlayer;

    private void Start(){
        this._audioPlayer = GetComponent<AudioSource>();
        this._audioPlayer.clip = this._clickSound;
    }

    public void OnStartPlay(){
        this._audioPlayer.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void OnClose(){
        this._audioPlayer.Play();
        Application.Quit();
    }
}
