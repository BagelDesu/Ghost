using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Chapter", fileName = "Chapter_")]
public class ChapterStruct : ScriptableObject
{
    public PuzzleStruct[] puzzles;

    public AudioClip[] preChapterDialouges;
    public AudioClip[] postChapterDialouges;
}
