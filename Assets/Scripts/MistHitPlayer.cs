using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistHitPlayer : MonoBehaviour
{
    public int playerHealth = 100;
    public int damage = 10;

    public int Damage
    {
        set { damage = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<Collider>().isTrigger = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Body")
        {
            Debug.Log("Hit player");
            
            Invoke("PlayerDamage", 3.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Body")
            CancelInvoke();
    }

    private void PlayerDamage()
    {
        Debug.Log("Player takes damage");
        this.playerHealth -= damage;

        if(this.playerHealth <= 0)
            EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_MIST_KILLS);

        Invoke("PlayerDamage", 3.0f);
    }
}
