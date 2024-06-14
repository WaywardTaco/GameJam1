using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FireflyScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Body")
        {

            this.prefab.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            Light light = this.prefab.GetComponent<Light>();
            if(light != null) light.color = Color.green;

        
            Parameters param = new Parameters();
            param.PutExtra("X_CHECK", this.gameObject.transform.position.x);
            param.PutExtra("Y_CHECK", this.gameObject.transform.position.y);
            param.PutExtra("Z_CHECK", this.gameObject.transform.position.z);
            EventBroadcaster.Instance.PostEvent(EventNames.Checkpoint.ON_COLLIDE_CHECKPOINT, param);
        }
    }
}
