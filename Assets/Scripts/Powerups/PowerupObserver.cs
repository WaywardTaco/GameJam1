using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupObserver : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private float timeEndPowerup = 30f;
    [SerializeField] private float massChangeAmount = 0.5f;
    [SerializeField] private int jumpChangeAmount = 2;
    [SerializeField] MistHitPlayer mist;


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
        this.mist.damage = 0;
        Invoke("OnCanisterEnd", timeEndPowerup);
    }

    private void OnCollideCapsule()
    {
        Rigidbody Mass = this.Player.GetComponent<Rigidbody>();
        if(Mass != null) Mass.mass -= massChangeAmount;
        Debug.Log("capsule");
        Invoke("OnCapsuleEnd", timeEndPowerup);
    }

    private void OnCollideJetpack()
    {
        PlayerMovement Jump = this.Player.GetComponent<PlayerMovement>();
        if (Jump != null) Jump.AddMaxAirJumps(jumpChangeAmount);
        Debug.Log("jetpack");
        Invoke("OnJetpackEnd", timeEndPowerup);
    }

    private void OnCanisterEnd()
    {
        this.mist.damage = 10;
        Debug.Log("end canister");
    }

    private void OnCapsuleEnd()
    {
        Rigidbody Body = this.Player.GetComponent<Rigidbody>();
        if (Body != null) Body.mass += massChangeAmount;
        Debug.Log("end capsule");
    }

    private void OnJetpackEnd()
    {
        PlayerMovement Jump = this.Player.GetComponent<PlayerMovement>();
        if (Jump != null) Jump.AddMaxAirJumps(-jumpChangeAmount);
        Debug.Log("end jetpack");
    }
}
