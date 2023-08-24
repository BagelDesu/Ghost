using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemInteraction : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    private string itemTemp = "";

    public void AddItemToInventory()
    {
        PlayerInventory.Instance.AddItem(itemName, itemTemp);
    }
}
