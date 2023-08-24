using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TemperatureField : MonoBehaviour
{
    [SerializeField]
    private TEMPERATURE fieldTemperature;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tool")
        {
            Thermometer thermometer = other.transform.GetComponent<Thermometer>();

            thermometer?.ChangeTemperature(fieldTemperature);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tool")
        {
            Thermometer thermometer = other.transform.GetComponent<Thermometer>();

            thermometer?.ChangeTemperature(TEMPERATURE.NEUTRAL);
        }
    }
}
