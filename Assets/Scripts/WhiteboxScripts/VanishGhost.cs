using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishGhost : MonoBehaviour
{
    [SerializeField]
    private float persistanceTime = 2f;

    private void Start()
    {
        Destroy(gameObject, persistanceTime);
    }
}
