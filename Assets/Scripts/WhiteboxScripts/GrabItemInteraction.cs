using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItemInteraction : MonoBehaviour
{
    private Tool tool;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tool = GetComponent<Tool>();
    }

    public void GrabItem()
    {
        rb.isKinematic = true;
        PlayerInventory.Instance.AddTool(tool.toolName, tool);
    }
}
