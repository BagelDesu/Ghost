using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quick and dirty way of making an Inventory system. Dictionary keeps the items for us to track.
/// We're using a singleton since we're 100% sure we won't have another inventory system here.
/// 
/// Note instead of making this a singleton, and using a heap memory, we can write the items to a json
/// file instead and reading it there. This might up performance, depending on how we do serializations.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    // TODO: Switch from Dict<string, string> to Dict<string, Item> incase we want to be able to equip the items.
    private Dictionary<string, string> Inventory = new Dictionary<string, string>();

    // A way for us to cycle through our tools.
    private Dictionary<string, Tool> obtainedTools = new Dictionary<string, Tool>();

    public HeldItem HeldItem;

    public static PlayerInventory Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private static PlayerInventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Singletons detected, deleting object. Please remove extra singleton from scene", this);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        HeldItem = GetComponent<HeldItem>();
    }

    public void AddTool(string toolName, Tool tool)
    {
        if (obtainedTools.ContainsKey(toolName))
        {
            // already has the tool in the inventory.
            return;
        }

        obtainedTools.Add(toolName, tool);
        HeldItem.HoldItem(tool.gameObject, toolName);
    }
    
    public void RemoveItem(string itemName)
    {
        if (Inventory.ContainsKey(itemName))
        {
            Inventory.Remove(itemName);
        }
    }

    public void RemoveItem(string[] itemNames)
    {
        foreach (string item in itemNames)
        {
            if (Inventory.ContainsKey(item))
            {
                Inventory.Remove(item);
            }
        }
    }

    public bool DoesItemExist(string itemName)
    {
        return Inventory.ContainsKey(itemName);
    }
    
    // TODO: replace method signiture with AddItem(string, Item) see note at top of file.
    public void AddItem(string itemName, string temp)
    {
        if (Inventory.ContainsKey(itemName))
        {
            // Similar named assets found, please consult design of item.
            return;
        }

        Inventory.Add(itemName, temp);
    }
}
