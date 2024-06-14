using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    
    [SerializeField] private AudioClip _clickSound;
    private AudioSource _audioPlayer;

    private void Start(){
        this._audioPlayer = GetComponent<AudioSource>();
        this._audioPlayer.clip = this._clickSound;
    }

    public void OnBackToMain(){
        this._audioPlayer.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
