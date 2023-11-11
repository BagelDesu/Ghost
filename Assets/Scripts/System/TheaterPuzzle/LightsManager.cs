using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    [SerializeField]
    private Light[] lights;

    private int currentLight = 0;

    private void Start()
    {
        if (lights.Length <= 0)
        {
            Debug.LogError("No Lights Assigned for light manager in theater puzzle");
        }
    }

    public void AdvanceLights()
    {
        if (currentLight >= lights.Length)
        {
            return;
        }
        lights[currentLight].enabled = false;
        currentLight++;
    }

    public void TurnOffAllLights()
    {
        foreach (var item in lights)
        {
            item.enabled = false;
        }
    }

    public void TurnOnAllLights()
    {
        foreach (var item in lights)
        {
            item.enabled = true;
        }
    }
}
