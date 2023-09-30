using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private CustomColor solveAnswer;

    [SerializeField]
    private ColorPuzzle puzzleManager;
    public void SendSolve()
    {
        puzzleManager.AddAnswer(solveAnswer);
    }
}
