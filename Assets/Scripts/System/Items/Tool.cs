using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public string toolName;

    [SerializeField]
    protected string toolDescription;
    protected bool isToolActive = false;
    

    public virtual void UseTool()
    {
        // What happens when the tool is used.

        isToolActive = !isToolActive;
    }

}
