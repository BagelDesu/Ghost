using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerGlobalSignalEvent : MonoBehaviour
{
    public UnityEvent Event_ToggleSignalOnUpdate;
    public float EventFrequency;

    private void Start()
    {
        if(Event_ToggleSignalOnUpdate == null)
        {
            Debug.LogWarning("Object does not contain events. killing object to conserve resources", this.gameObject);
            Destroy(this.gameObject);
            return;
        }

        StartCoroutine(CustomUpdate());
    }

    private IEnumerator CustomUpdate()
    {
        while (true)
        {
            Event_ToggleSignalOnUpdate.Invoke();
            yield return new WaitForSeconds(EventFrequency);
        }
    } 
}
