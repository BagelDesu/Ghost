using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightsInteraction : MonoBehaviour
{
    private bool isLightOn = true;

    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private Light light;
    [SerializeField]
    private MeshRenderer mesh;

    private void Start()
    {

    }

    public void ToggleLights()
    {
        isLightOn = !isLightOn;

        if (isLightOn)
        {
            light.enabled = true;
            mesh.material = materials[1];
        }
        else
        {
            light.enabled = false;
            mesh.material = materials[0];
        }
    }

}
