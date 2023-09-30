using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Light flashlight;
    private void Start()
    {
        flashlight = GetComponent<Light>();
    }

    public void Toggle()
    {
        flashlight.enabled = !flashlight.enabled;
    }
}
