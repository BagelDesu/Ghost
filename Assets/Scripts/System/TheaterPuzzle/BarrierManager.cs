using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] barriers;
    private int currentBarrier = 0;

    public void AdvanceBarriers()
    {
        if (currentBarrier >= barriers.Length)
        {
            return;
        }
        barriers[currentBarrier].SetActive(true);
        currentBarrier++;
    }

    public void ResetBarriers()
    {
        foreach (GameObject item in barriers)
        {
            item.SetActive(false);
        }
    }
}
