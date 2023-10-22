using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BagelDesu.Systems.Collisions
{
    [RequireComponent(typeof(Collider))]
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent Event_Interaction;

        [SerializeField]
        private bool isPlayerInRange = false;

        private bool isInteractionLocked = false;

        public string InteractionTooltip;
        public Sprite InteractionImage;
        
        public void InvokeInteractions()
        {
            if (Event_Interaction != null && isPlayerInRange && !isInteractionLocked)
            {
                Event_Interaction.Invoke();
            }
        }

        public void SetPlayerInRange(bool status)
        {
            isPlayerInRange = status;
        }

        public void SetPlayerInRangeTrue()
        {
            isPlayerInRange = true;
        }
        public void SetPlayerInRangeFalse()
        {
            isPlayerInRange = false;
        }

        public void SetInteractionLocked(bool status)
        {
            isInteractionLocked = status;
        }
    }
}