using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    STORY,
    CHAPTER,
    PUZZLE
}

public class Objective : MonoBehaviour
{
    public bool IsObjectiveCompleted { get { return isObjectiveCompleted; } private set { isObjectiveCompleted = value; } }

    public ObjectiveType ObjectiveType;

    public string ObjectiveName;

    private bool isObjectiveCompleted = false;

    public void CompleteObjective() 
    { 
        isObjectiveCompleted = true;
        // have the dialouge play here for completed objectives?
    }
}
