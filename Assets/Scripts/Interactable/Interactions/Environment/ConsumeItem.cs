using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumeItem : MonoBehaviour
{
    public UnityEvent Event_ItemConsumed;
    public UnityEvent Event_ItemFailed;

    public string[] ItemsToConsume;

    public void TryConsumeItem()
    {
        foreach (string item in ItemsToConsume)
        {
            if (!PlayerInventory.Instance.DoesItemExist(item))
            {
                Event_ItemFailed?.Invoke();
                return;
            }
        }

        PlayerInventory.Instance.RemoveItem(ItemsToConsume);
        Event_ItemConsumed?.Invoke();
    }
}
