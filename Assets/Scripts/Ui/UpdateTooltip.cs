using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTooltip : MonoBehaviour
{
    [SerializeField]
    private Sprite toolTipSprite;
    [SerializeField]
    private string toolTipDesc;

    public void UpdateCurrentTooltip()
    {
        InteractTooltipUi.Instance.UpdateTooltip(toolTipDesc);
        InteractTooltipUi.Instance.UpdateCursorImage(toolTipSprite);
    }
}
