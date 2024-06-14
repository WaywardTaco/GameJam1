using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistHitPlayer : MonoBehaviour
{
    public int playerHealth = 100;
    public int damage = 10;

    [SerializeField] private AudioClip _hurtSound;
    private AudioSource _audioPlayer;

    public int Damage
    {
        set { damage = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<Collider>().isTrigger = true;

        this._audioPlayer = GetComponent<AudioSource>();
        this._audioPlayer.clip = _hurtSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Body")
        {
            Debug.Log("Hit player");
            EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_COLLIDE_MIST);

            Invoke("PlayerDamage", 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Body")
        {
            EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_MIST_EXIT);
            CancelInvoke();
        }
            
    }

    private void PlayerDamage()
    {
        Debug.Log("Player takes damage");
        this.playerHealth -= damage;
        if(this.damage > 0){
            this._audioPlayer.Play();
            EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_MIST_DAMAGE);
        }
        Debug.Log(playerHealth);


        if(this.playerHealth <= 0)
            EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_MIST_KILLS);

        Invoke("PlayerDamage", 3.0f);
    }
}
