using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MistCollide.ON_MIST_KILLS,listenMistKill);
    }

    private void OnDisable() {    
        EventBroadcaster.Instance.RemoveObserver(EventNames.MistCollide.ON_MIST_KILLS);
    }

    private void listenMistKill(){
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("DeathScene");
    }
}
