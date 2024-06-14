using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Body")
        {
            EventBroadcaster.Instance.PostEvent(EventNames.Powerups.ON_COLLIDE_CANISTER);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
