using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private CustomColor solveAnswer;

    [SerializeField]
    private PuzzleManager puzzleManager;
    public void SendSolve()
    {
        puzzleManager.AddAnswer(solveAnswer);
    }
}
