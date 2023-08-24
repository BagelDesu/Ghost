using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomColor
{
    BLUE,
    RED,
    GREEN
}

public enum CustomDir
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
}

[CreateAssetMenu(menuName = "Custom/Puzzle", fileName = "Puzzle_")]
public class PuzzleStruct : ScriptableObject
{
    public float time = 0;
    public CustomColor[] colors;
    public CustomDir direction;
    public AudioClip successDialouge;
    public AudioClip failDialouge;
}
