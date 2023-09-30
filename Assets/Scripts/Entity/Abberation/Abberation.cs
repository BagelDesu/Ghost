using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abberation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Abberations;
    private int currentAbberation = 0;

    public void AdvanceAbberation()
    {
        if (currentAbberation > 0)
        {
            Abberations[currentAbberation - 1].SetActive(false);
        }
        Abberations[currentAbberation].SetActive(true);
        currentAbberation++;
    }

}
