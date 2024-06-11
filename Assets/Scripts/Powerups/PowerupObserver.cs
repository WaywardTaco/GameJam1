using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupObserver : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Powerups.ON_COLLIDE_CANISTER, this.OnCollideCanister);
        EventBroadcaster.Instance.AddObserver(EventNames.Powerups.ON_COLLIDE_CAPSULE, this.OnCollideCapsule);
        EventBroadcaster.Instance.AddObserver(EventNames.Powerups.ON_COLLIDE_JETPACK, this.OnCollideJetpack);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Powerups.ON_COLLIDE_CANISTER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Powerups.ON_COLLIDE_CAPSULE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Powerups.ON_COLLIDE_JETPACK);
    }

    private void OnCollideCanister()
    {
        Debug.Log("canister");
    }

    private void OnCollideCapsule()
    {
        Debug.Log("capsule");
    }

    private void OnCollideJetpack()
    {
        Debug.Log("jetpack");
    }
}
