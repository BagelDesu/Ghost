using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSubTooltip : MonoBehaviour
{
    [SerializeField]
    private string toolTipDesc;

    public void UpdateCurrentTooltip()
    {
        InteractTooltipUi.Instance.FireSubTooltipRequest(toolTipDesc);
    }
}
