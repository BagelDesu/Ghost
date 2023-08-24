using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItem : MonoBehaviour
{
    private GameObject ItemReference;
    private string ItemName;

    [SerializeField]
    private Transform RightHand;

    public void HoldItem(GameObject obj, string itmName)
    {
        ItemReference = obj;
        ItemName = itmName;

        ItemReference.transform.parent = RightHand;
        ItemReference.transform.SetPositionAndRotation(RightHand.transform.position, RightHand.transform.rotation);
    }

    public void UseHeldTool(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            Tool tool = ItemReference?.GetComponent<Tool>();

            if(tool != null)
            {
                tool.UseTool();
            }
        }
    }

    public Transform GetRightHand()
    {
        return RightHand;
    }

    public string GetHeldItem()
    {
        return ItemName;
    }

}
