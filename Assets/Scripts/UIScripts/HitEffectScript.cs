using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffectScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

        EventBroadcaster.Instance.AddObserver(EventNames.MistCollide.ON_MIST_DAMAGE, this.OnDamage);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MistCollide.ON_MIST_DAMAGE);
    }

    private void OnDamage()
    {
        Debug.Log("on damage called");
        panel.SetActive(true);
        Invoke("Exit", 0.15f);
    }

    private void Exit()
    {
        panel.SetActive(false);
    }
}
