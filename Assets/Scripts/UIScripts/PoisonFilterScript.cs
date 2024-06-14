using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PoisonFilterScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

        EventBroadcaster.Instance.AddObserver(EventNames.MistCollide.ON_COLLIDE_MIST, this.OnCollide);
        EventBroadcaster.Instance.AddObserver(EventNames.MistCollide.ON_MIST_EXIT, this.OnCollideExit);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MistCollide.ON_COLLIDE_MIST);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MistCollide.ON_MIST_EXIT);
    }

    private void OnCollide()
    {
        Debug.Log("on collide called");
        panel.SetActive(true);
    }

    private void OnCollideExit()
    {
        panel.SetActive(false);
    }
}
