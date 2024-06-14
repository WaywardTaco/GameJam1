using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Body")
        {
            EventBroadcaster.Instance.PostEvent(EventNames.Powerups.ON_COLLIDE_CAPSULE);
            Destroy(this.transform.parent.gameObject);
        }    
    }
}
