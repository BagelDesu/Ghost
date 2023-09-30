using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzleManager : MonoBehaviour
{

    // TODO we're manually assigning the puzzle manager, we should have a more automatic/dynamic way of assigning it instead.
    [SerializeField]
    private ColorPuzzle puzzleManager;

    [SerializeField]
    private ChapterStruct[] playableChapters;

    private int currentChapter = 0;

    private bool isChapterFresh = true;

    // TEMP not sure what to do with this yet, but felt like we should have something like this here.
    private List<Objective> chapterObjectives = new List<Objective>();
    // EVENTS

    public UnityEvent OnChapterStart;
    public UnityEvent OnChapterAdvance;

    public UnityEvent OnChapterSuccess;
    public UnityEvent OnChapterFail;

    private void Start()
    {
        puzzleManager.OnFinishedPuzzles.AddListener(AdvanceChapter);
        if (puzzleManager == null) { Debug.LogError("No Puzzle Manager assigned. please fix.", this); }
    }

    private void AdvanceChapter(int code)
    {
        isChapterFresh = true;
        // we want to advance the chapter regardless of what happens.

        if (currentChapter < playableChapters.Length)
        {
            StartChapter(playableChapters[currentChapter]);
        }

        currentChapter++;

        switch (code)
        {
            // if the puzzles were a success we want to continue it until there are no more puzzles.
            case 0:
                if (currentChapter >= playableChapters.Length)
                {
                    // if the player was able to advance the chapters all the way through it's a good end aka open the door.
                    OnChapterSuccess?.Invoke();
                    currentChapter = 0;
                }
                break;
            // if the player gives the wrong answer give a "fail" flag.
            case 1:
                //OnChapterFail?.Invoke();
                //currentChapter = 0;
                break;
            // if the player runs out of time, give a "fail" flag.
            case 2:
                //OnChapterFail?.Invoke();
                //currentChapter = 0;

                break;
            default:
                Debug.LogError("Invalid advance code.");
                break;
        }
    }

    public void StartChapter()
    {
        if (playableChapters.Length <= 0)
        {
            Debug.LogError("Missing playable chapters", this);
            return;
        }
        StartChapter(playableChapters[currentChapter]);
        currentChapter++;
    }

    public void StartChapter(ChapterStruct data)
    {
        // this starts the chapter, we need to check if there are any pre-dialouges in it before we can move forward.
        if (data.preChapterDialouges.Length > 0)
        {
            // if we have dialogue loaded in the chapter, go through the pre-dialouge'd iterator.
            StartCoroutine(PreDialougedChapter(data));
        }
        else
        {
            // if we don't just load the puzzles.
            LoadPuzzles(data.puzzles);
        }
    }

    IEnumerator PreDialougedChapter(ChapterStruct data)
    {
        DialougeManager.Instance.LoadDialougeClipsBehind(new List<AudioClip>(data.preChapterDialouges), false, true);
        // make sure we wait for the audio to stop playing, if there is one.
        yield return new WaitUntil(() => { return DialougeManager.Instance.IsQueueEmpty; });

        LoadPuzzles(data.puzzles);
    }

    private void LoadPuzzles(PuzzleStruct[] data)
    {
        puzzleManager.LoadPuzzleData(data);

        if (isChapterFresh)
        {
            OnChapterStart?.Invoke();
            isChapterFresh = false;
        }
        else
        {
            OnChapterAdvance?.Invoke();
        }
    } 
}
