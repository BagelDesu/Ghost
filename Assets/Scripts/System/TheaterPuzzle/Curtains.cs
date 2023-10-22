using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  TODO need to refactor so that the curtains itself contains a bool that i can read instead of creating it on multiple
///  scripts
/// </summary>
public class Curtains : MonoBehaviour
{
    private Animator animator;

    public UnityEvent OnCurtainFinishedSwitch;

    public UnityEvent OnCurtainStartedSwitch;

    public UnityEvent OnCurtainSwitchClosed;

    public UnityEvent OnCurtainOpened;

    public UnityEvent OnCurtainClosed;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CurtainFinishedSwitch()
    {
        OnCurtainFinishedSwitch?.Invoke();
    }

    public void CurtainStartedSwitch()
    {
        OnCurtainStartedSwitch?.Invoke();
    }

    public void CurtainSwitchClosed()
    {
        OnCurtainSwitchClosed?.Invoke();
    }
}
