using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the overall story management.
/// 
/// Has a reference to the chapter manager to call in which ever chapter is supposed to go, and in which order.
/// can also load chapters.
/// </summary>
public class StoryManager : MonoBehaviour
{
    // TODO: We'll probably want to create a "Story" struct to hold all the chapters, chapter names, and any special flags we want to load the chapter with
    // but for now we'll just have a simple array, aka " we have x amount of chapters, and if you want to skip to any just input the appropriate int"
    // but later i want to have it so that we can load the chapter via id or chapter name
    [SerializeField]
    private ChapterStruct[] playableChapters;

    private int currentChapter = 0;

    // TODO we're manually assigning the puzzle manager, we should have a more dynamic/automatic way of assigning it instead.
    [SerializeField]
    private ChapterManager chapterManager;


    // EVENTS

    /// <summary>
    /// provides an int that represents how the story started
    /// 
    /// 0 - Start from first Chapter
    /// 1 - Load Chapter
    /// 
    /// </summary>
    public UnityEvent<int> OnStoryStart;

    /// <summary>
    /// provides an int that represents which chapter is currently playing
    /// 
    /// 0 to chapters length
    /// 
    /// </summary>
    public UnityEvent<int> OnStoryAdvance;

    /// <summary>
    /// provides an int that represents how the story ends
    /// 
    /// 0 - Good End ( player got all the objectives correct )
    /// 1 - Bad End ( player failed to complete objectives )
    /// 2 - Timer End ( player ran out of time )
    /// 
    /// with this we may be able to implement multiple endings.
    /// might be more human friendly to use string return here,
    /// but i'm the only one working on it so we can keep using
    /// ints.
    /// </summary>
    public UnityEvent<int> OnStoryEnd;


    private void Start()
    {
        if (chapterManager == null)
        {
            Debug.LogError("missing chapter manager. please fix", this);
        }

        // Automatically advances the chapter to the next assigned one after it completes.
        chapterManager.OnChapterComplete.AddListener(AdvanceStory);
        StartStory();
    }

    public void StartStory()
    {
        if(playableChapters.Length <= 0)
        {
            Debug.LogError("Missing playable chapters", this);
            return;
        }
        chapterManager.StartChapter(playableChapters[currentChapter]);
        currentChapter++;
        OnStoryStart?.Invoke(0);
    }

    /// <summary>
    /// Start the story at the specified chapter aka "Load".
    /// </summary>
    /// <param name="chapter">position of the chapter in the array, starts at 0</param>
    public void StartStory(int chapter)
    {
        if (chapter > playableChapters.Length)
        {
            Debug.LogError("Chapter Does not exist");
            return;
        }

        chapterManager.StartChapter(playableChapters[chapter]);
        currentChapter = chapter;
        OnStoryStart?.Invoke(1);
    }

    /// <summary>
    /// Used to incrementally step through the chapters.
    /// </summary>
    public void AdvanceStory(int code)
    {
        switch (code)
        {
            case 0:

                if (currentChapter < playableChapters.Length)
                {
                    chapterManager.StartChapter(playableChapters[currentChapter]);
                    OnStoryAdvance?.Invoke(currentChapter);
                }
                currentChapter++;

                if (currentChapter >= playableChapters.Length)
                {
                    // if the player was able to advance the chapters all the way through it's a good end.
                    OnStoryEnd?.Invoke(0);
                }

                break;
            case 1:

                OnStoryEnd?.Invoke(1);

                break;
            case 2:

                OnStoryEnd?.Invoke(2);

                break;
            default:

                OnStoryEnd?.Invoke(-1);

                break;
        }
    }
}
