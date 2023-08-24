using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractTooltipUi : MonoBehaviour
{
    [SerializeField]
    private UnityEvent Event_OnEnableRequest;
    [SerializeField]
    private UnityEvent Event_OnDisableRequest;

    [SerializeField]
    private TextMeshProUGUI SubToolTip;
    
    [SerializeField]
    private Sprite CursorSprite;
    
    private Image CursorImage;

    private TextMeshProUGUI ToolTip;

    public static InteractTooltipUi Instance
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

    private static InteractTooltipUi instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple Singletons detected, deleting object. Please remove extra singleton from scene", this);
            Destroy(this.gameObject);
            return;
        }

        CursorImage = GetComponentInChildren<Image>(true);
        ToolTip = GetComponentsInChildren<TextMeshProUGUI>(true)[1];
        Instance = this;
        CursorImage.sprite = CursorSprite;
    }

    public void TurnOffInteractTooltip()
    {
        if (Event_OnDisableRequest != null)
        {
            Event_OnDisableRequest.Invoke();
        }
    }

    public void TurnOnInteractTooltip()
    {
        if (Event_OnEnableRequest != null)
        {
            Event_OnEnableRequest.Invoke();
        }
    }

    public void ResetTooltip()
    {
        ToolTip.text = "";
        CursorImage.sprite = CursorSprite;
    }

    public void UpdateCursorImage(Sprite sSprite)
    {
        if (sSprite == null)
        {
            return;
        }
        CursorImage.sprite = sSprite;
    }

    public void UpdateTooltip(string toolTip)
    {
        ToolTip.text = toolTip;
    }

    public void FireSubTooltipRequest(string subTooltipDesc)
    {
        SubToolTip.text = subTooltipDesc;

        StopAllCoroutines();
        StartCoroutine(SubtooltipRequestTimer());
    }

    private IEnumerator SubtooltipRequestTimer()
    {
        SubToolTip.gameObject.SetActive(true);

        float t = 0;

        while (t < 2f)
        {
            t += Time.deltaTime;

            yield return null;
        }

        SubToolTip.gameObject.SetActive(false);

    }
}
