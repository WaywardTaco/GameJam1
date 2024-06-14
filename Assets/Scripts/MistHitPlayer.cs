using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistHitPlayer : MonoBehaviour
{
    public int playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<Collider>().isTrigger = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Body")
        {
            Debug.Log("Hit player");
            this.playerHealth -= 10;

            if(this.playerHealth <= 0)
                EventBroadcaster.Instance.PostEvent(EventNames.MistCollide.ON_MIST_KILLS);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
