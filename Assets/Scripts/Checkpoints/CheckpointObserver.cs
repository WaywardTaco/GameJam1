using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObserver : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private AudioClip _CheckpointSFX;
    private Vector3 Position = Vector3.zero;
    private AudioSource _audioPlayer;

    void Start()
    {
        this.Position = Player.transform.position;
        this._audioPlayer = GetComponent<AudioSource>();
        this._audioPlayer.clip = this._CheckpointSFX;
        EventBroadcaster.Instance.AddObserver(EventNames.Checkpoint.ON_COLLIDE_CHECKPOINT, this.OnCollideCheckpoint);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Checkpoint.ON_COLLIDE_CHECKPOINT);
    }

    private void OnCollideCheckpoint(Parameters param)
    {
        float x = param.GetFloatExtra("X_CHECK", 0f);
        float y = param.GetFloatExtra("Y_CHECK", 0f);
        float z = param.GetFloatExtra("Z_CHECK", 0f);


        if (y > this.Position.y)
        {
            this.Position = new Vector3(x, y, z);
            this._audioPlayer.Play();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e pressed");
            this.Player.SetActive(false);
            this.Player.transform.position = this.Position;
            this.Player.SetActive(true);

        }
    }
}
 
