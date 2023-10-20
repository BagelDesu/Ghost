using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorName
{
    FATHER,
    CHILD,
    MOTHER,
    KINGINYELLOW
}

public class Actor : MonoBehaviour
{
    /// <summary>
    /// -1 is when actor is not in the scene
    /// 0 is the first position of the actor.
    /// </summary>
    private int currentPosition = -1;

    public int CurrentPosition { get { return currentPosition; } private set { currentPosition = value; } }

    /// <summary>
    /// what we spawn in based on which position the actor is in. co-related to the "currentPosition" of the actor.
    /// </summary>
    [SerializeField]
    private List<GameObject> actorPrefabs;

    [SerializeField]
    private ActorName actorName;

    public ActorName ActorName { get { return actorName; } private set { actorName = value; } }

    private void Start()
    {
        if(actorPrefabs.Count <= 0)
        {
            Debug.LogError("Empty Actor in theater puzzle.", this);
        }
    }

    /// <summary>
    /// reset actor to 0 and show them in the scene. 
    /// </summary>
    public void ActivateActor()
    {
        currentPosition = 0;

        foreach (GameObject actorPos in actorPrefabs)
        {
            actorPos.SetActive(false);
        }

        actorPrefabs[currentPosition].SetActive(true);
    }

    /// <summary>
    ///  remove all instance of the actor out of the scene.
    /// </summary>
    public void DisableActor()
    {
        currentPosition = -1;
        foreach(GameObject actorPos in actorPrefabs)
        {
            actorPos.SetActive(false);
        }
    }

    /// <summary>
    /// moves the actor to the next position.
    /// </summary>
    public void AdvanceActor()
    {
        currentPosition++;
        if (currentPosition >= actorPrefabs.Count)
        {
            DisableActor();
            return;
        }
        foreach (GameObject actorPos in actorPrefabs)
        {
            actorPos.SetActive(false);
        }
        actorPrefabs[currentPosition].SetActive(true);
    }

    /// <summary>
    /// moves the actor to the previous position.
    /// </summary>
    public void RegressActor()
    {
        currentPosition--;
        if (currentPosition <= 0)
        {
            currentPosition = actorPrefabs.Count -1;
        }
        foreach (GameObject actorPos in actorPrefabs)
        {
            actorPos.SetActive(false);
        }
        actorPrefabs[currentPosition].SetActive(true);
    }
}
