using BagelDesu.Systems.Collisions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask RaycastMask;
    public float RaycastMaxRange;
    public GameObject RaycastOrigin;

    private bool shouldLockInteractions;


    private bool isPlayerLookingAtInteractObject = false;
    private InteractableObject interactableObject;
    private void FixedUpdate()
    {
        DrawRaycast();
    }

    // Try to keep this as light as we can since this is running on FixedUpdate. Keep Intense calculations outside of this, as much as you can.
    private void DrawRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(RaycastOrigin.transform.position, RaycastOrigin.transform.forward, out hit, RaycastMaxRange, RaycastMask))
        {
            interactableObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                isPlayerLookingAtInteractObject = true;

                // quick way of changing the UI tooltip and stuff, not sure if this is the cleanest way of doing it.
                InteractTooltipUi.Instance.UpdateCursorImage(interactableObject.InteractionImage);
                InteractTooltipUi.Instance.UpdateTooltip(interactableObject.InteractionTooltip);
                InteractTooltipUi.Instance.TurnOnInteractTooltip();
            }
            else
            {
                Debug.Log(hit.transform.name, hit.collider);
                isPlayerLookingAtInteractObject = false;
                InteractTooltipUi.Instance.TurnOffInteractTooltip();
            }
        }
        else
        {
            isPlayerLookingAtInteractObject = false;
            InteractTooltipUi.Instance.TurnOffInteractTooltip();
        }

    }

    public void LockInteractions()
    {
        shouldLockInteractions = true;
    }

    public void UnlockInteractions()
    {
        shouldLockInteractions = false;
    }

    public void OnInteractKeyPress(InputAction.CallbackContext context)
    {
        if (context.started && !shouldLockInteractions)
        {
            if (interactableObject != null && isPlayerLookingAtInteractObject)
            {
                interactableObject.InvokeInteractions();
                InteractTooltipUi.Instance.ResetTooltip();
            }
        }
    }
}
