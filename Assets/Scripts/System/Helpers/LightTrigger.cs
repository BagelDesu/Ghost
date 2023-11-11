using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private Light[] lights;
    private void Start()
    {
        lights = GetComponentsInChildren<Light>(true);
    }

    public void DisableAllLights()
    {
        foreach(Light l in lights)
        {
            l.enabled = false;
        }
    }

    public void EnableAllLights()
    {
        foreach (Light l in lights)
        {
            l.enabled = true;
        }
    }
}
