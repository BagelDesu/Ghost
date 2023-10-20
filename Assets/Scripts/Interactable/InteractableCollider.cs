using UnityEngine;
using UnityEngine.Events;

namespace BagelDesu.Systems.Collisions
{
    /// <summary>
    /// Broadcasts Triggers into events that we can assign in the Unity Editor.
    /// 
    /// Purpose is to make a script that we can reuse between all interactables, using ANY colliders
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableCollider : MonoBehaviour
    {
        
        [SerializeField]
        private UnityEvent Event_PlayerEnter;
        [SerializeField]
        private UnityEvent Event_PlayerExit;

        [SerializeField]
        private bool findAllInteractableObjects = false;

        [SerializeField]
        private LayerMask BlacklistedLayers;            //TODO: Implement Layer filtering.

        private void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;

            if (findAllInteractableObjects)
            {
                InteractableObject[] interactableObjects = gameObject.transform.parent.GetComponentsInChildren<InteractableObject>(true);

                foreach (InteractableObject obj in interactableObjects)
                {
                    Event_PlayerEnter.AddListener(obj.SetPlayerInRangeTrue);
                    Event_PlayerExit.AddListener(obj.SetPlayerInRangeFalse);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Event_PlayerEnter != null && other.CompareTag("Player"))
            {
                Event_PlayerEnter.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Event_PlayerExit != null && other.CompareTag("Player"))
            {
                Event_PlayerExit.Invoke();
            }
        }
    }
}
