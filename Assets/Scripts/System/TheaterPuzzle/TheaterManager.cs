using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheaterManager : MonoBehaviour
{
    // EXTERNAL
    [SerializeField]
    private string keypadPassword = "0000";

    [SerializeField]
    private Actor[] actors;

    [SerializeField]
    private List<Set> sets = new List<Set>();

    [SerializeField]
    private Keypad keypad;

    [SerializeField]
    private TimerScreen[] theaterScreens;

    // INTERNAL
    /// <summary>
    /// internal dictionary that will be used for ease of indexing.
    /// </summary>
    private Dictionary<ActorName, Actor> actorDict = new Dictionary<ActorName, Actor>();
    private int currentSet = 0;
    

    // EVENTS
    public UnityEvent OnPuzzleFinished;
    public UnityEvent OnSetSolved;
    public UnityEvent OnSetFailed;

    private bool setFinished = false;

    private void Start()
    {
        // loading our actors in our dictionary for easy indexing.
        foreach (Actor act in actors)
        {
            actorDict.Add(act.ActorName, act);
        }

        keypad.KeypadPassword = keypadPassword;
    }

    public void SolveSet()
    {
        if (setFinished)
        {
            return;
        }

        List<bool> answers = new List<bool>();
        foreach(TheaterSetSO TSet in sets[currentSet].TheaterSet)
        {
            if (actorDict[TSet.actorName].CurrentPosition == TSet.position)
            {
                answers.Add(true);
            }
            else
            {
                answers.Add(false);
            }
        }

        if (answers.Contains(false))
        {
            // wrong answer.
            OnSetFailed?.Invoke();                 
        }
        else
        {
            OnSetSolved?.Invoke();
            ResetPuzzle();
            AdvanceSet();
        }
    }

    private void AdvanceSet()
    {
        sets[currentSet].gameObject.SetActive(false);
        theaterScreens[currentSet].UpdateText(keypadPassword[currentSet].ToString());
        currentSet++;

        if (currentSet >= sets.Count && setFinished == false)
        {
            OnPuzzleFinished?.Invoke();
            setFinished = true;
            return;
        }

        sets[currentSet].gameObject.SetActive(true);
    }

    public void ResetPuzzle()
    {
        foreach (Actor item in actors)
        {
            item.DisableActor();
        }
    }
}
