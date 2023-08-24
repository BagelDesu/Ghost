using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TEMPERATURE
{
    COLD,
    NEUTRAL,
    HOT
}

public class Thermometer : Tool
{
    [SerializeField]
    private Material coldMat;
    [SerializeField]
    private Material neutralMat;
    [SerializeField]
    private Material hotMat;


    private MeshRenderer rend = null;

    private void OnEnable()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void ChangeTemperature(TEMPERATURE temp)
    {
        switch (temp)
        {
            case TEMPERATURE.COLD:
                rend.material = coldMat;
                break;
            case TEMPERATURE.NEUTRAL:
                rend.material = neutralMat;
                break;
            case TEMPERATURE.HOT:
                rend.material = hotMat;
                break;
            default:
                break;
        }
    }

}
