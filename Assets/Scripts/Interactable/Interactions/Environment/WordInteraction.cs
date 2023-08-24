using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInteraction : MonoBehaviour
{
    private float holdDuration; // TODO: Implement button Hold duration

    [SerializeField]
    private float persistanceTime;
    
    // words falling
    private Rigidbody rigidbody;
    private Collider collider;

    // ghost spawning
    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    private Transform ghostSpawnLocation;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        collider.enabled = false;

        Instantiate(ghost, ghostSpawnLocation.position, ghostSpawnLocation.rotation, null);

        Destroy(gameObject, persistanceTime);
    }
}
