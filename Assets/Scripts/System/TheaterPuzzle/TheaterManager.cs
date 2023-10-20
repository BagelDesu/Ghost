using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheaterManager : MonoBehaviour
{
    // EXTERNAL
    [SerializeField]
    private Actor[] actors;

    [SerializeField]
    private List<GameObject> sets = new List<GameObject>();

    // INTERNAL
    /// <summary>
    /// internal dictionary that will be used for ease of indexing.
    /// </summary>
    private Dictionary<Actor, ActorName> actorDict = new Dictionary<Actor, ActorName>();

    private void Start()
    {
        // loading our actors in our dictionary for easy indexing.
        foreach (Actor act in actors)
        {
            actorDict.Add(act, act.ActorName);
        }
    }

    public void SolvePuzzle()
    {

    }
}
