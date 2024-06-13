using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FireflyScript : MonoBehaviour
{
    private string PlayerName = "Player";

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject Player = GameObject.Find(PlayerName);
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        if (other.gameObject.name == "Body")
        {
            Parameters param = new Parameters();
            param.PutExtra("X_CHECK", this.gameObject.transform.position.x);
            param.PutExtra("Y_CHECK", this.gameObject.transform.position.y);
            param.PutExtra("Z_CHECK", this.gameObject.transform.position.z);
            EventBroadcaster.Instance.PostEvent(EventNames.Checkpoint.ON_COLLIDE_CHECKPOINT, param);
        }
    }
}
