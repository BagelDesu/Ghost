using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlockade : MonoBehaviour
{
    [SerializeField]
    private float ExpulsionForce = 20f;

    [SerializeField]
    private Transform ExpulsionOrigin;

    private Rigidbody[] rigidbodies;
    
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();   
    }

    public void OpenBlockade()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(ExpulsionForce, ExpulsionOrigin.position, 5f,1f,ForceMode.Force);
        }
    }
}
