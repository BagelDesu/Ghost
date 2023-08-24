using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles control of the chapter that is being played right now.
/// </summary>
public class ChapterManager : MonoBehaviour
{
  
    // TODO we're manually assigning the puzzle manager, we should have a more automatic/dynamic way of assigning it instead.
    [SerializeField]
    private PuzzleManager puzzleManager;

    private bool isChapterFresh = true;

    [SerializeField]
    private AudioSource dialougeSource;

    private int currentAudio = 0;

    // EVENTS

    public UnityEvent OnChapterStart;
    public UnityEvent OnChapterAdvance;
    public UnityEvent<int> OnChapterComplete;

    private void Start()
    {
        puzzleManager.OnFinishedPuzzles.AddListener(ChapterFinished);
        if (puzzleManager == null) { Debug.LogError("No Puzzle Manager assigned. please fix.", this); }
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
        // make sure we wait for the audio to stop playing, if there is one.
        yield return new WaitUntil(() => { return !dialougeSource.isPlaying; });

        // assign the clip
        dialougeSource.clip = data.preChapterDialouges[currentAudio];

        // play it
        dialougeSource.Play();

        // increase our counter keeping track of which dialouge is playing
        currentAudio++;

        // check if we have any remaining dialouges left, if not load the puzzle, else start a recursion and try again.
        if (currentAudio < data.preChapterDialouges.Length)
        {
            StartCoroutine(PreDialougedChapter(data));
        }
        else
        {
            yield return new WaitUntil(() => { return !dialougeSource.isPlaying; });
            LoadPuzzles(data.puzzles);
        }
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

    public void ChapterFinished(int code)
    {
        // could add in multiple conditions before the chapter can get completed. but for now we'll add in the simple version of just calling our finished event.
        // also why i'm using a switch case rn, if we end up not wanting multiple endings or conditions we can just leave this as is. or reformat it to just echo the event
        // with the same code.
        switch (code)
        {
            case 0:
                // success
                OnChapterComplete?.Invoke(0);
                break;
            case 1:
                // fail
                OnChapterComplete?.Invoke(1);
                break;
            case 2:
                // time out
                OnChapterComplete?.Invoke(2);
                break;
            default:
                // error 
                OnChapterComplete?.Invoke(-1);

                break;
        }

        //reset the audio counter
        currentAudio = 0;

        // reset chapter status
        isChapterFresh = true;
    }
}
