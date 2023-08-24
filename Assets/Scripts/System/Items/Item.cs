using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Item Class. Should be used for inventory purposes.
/// </summary>
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public Sprite image;
}
