using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    private string keypadPassword = "0000";

    private string currentPasswordGuess = "";

    [SerializeField]
    private TextMeshProUGUI keypadDisplay;

    private bool isKeypadSolved = false;

    public UnityEvent OnKeypadSolve;

    public UnityEvent OnKeypadFail;

    public void AppendValue(string value)
    {
        if (isKeypadSolved)
        {
            return;
        }

        if (currentPasswordGuess.Length > keypadPassword.Length-1)
        {
            // force a guess
            if (currentPasswordGuess == keypadPassword)
            {
                OnKeypadSolve?.Invoke();
                isKeypadSolved = true;
            }
            else
            {
                OnKeypadFail?.Invoke();
                ResetKeypad();
            }
        }

        currentPasswordGuess += value;
        keypadDisplay.text = currentPasswordGuess;
    }

    public void SolveKeypad()
    {
        if (isKeypadSolved)
        {
            return;
        }

        if (currentPasswordGuess == keypadPassword)
        {
            OnKeypadSolve?.Invoke();
            isKeypadSolved = true;
        }
        else
        {
            OnKeypadFail?.Invoke();
            ResetKeypad();
        }
    }

    public void ResetKeypad()
    {
        if (isKeypadSolved)
        {
            return;
        }

        currentPasswordGuess = "";
        keypadDisplay.text = "0000";
    }
}
