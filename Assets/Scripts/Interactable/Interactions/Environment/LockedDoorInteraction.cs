using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedDoorInteraction : MonoBehaviour
{
    public bool isDoorLocked = true;

    public UnityEvent OpenEvent;
    public UnityEvent FailedOpenEvent;

    public void UnlockDoor()
    {
        isDoorLocked = false;
    }

    public void OpenDoor()
    {
        if (!isDoorLocked)
        {
            OpenEvent?.Invoke();
        }
        else
        {
            FailedOpenEvent?.Invoke();
        }
    }
}
