using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool IsObjectiveCompleted { get { return isObjectiveCompleted; } private set { isObjectiveCompleted = value; } }

    private bool isObjectiveCompleted = false;

    public void CompleteObjective() 
    { 
        isObjectiveCompleted = true;
    }
}
