using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialougeManager : MonoBehaviour
{
    private List<AudioClip> clipQueue = new List<AudioClip>();
    private int audioCount = 0;

    private Coroutine currentPlayingCoroutine;

    [SerializeField]
    private AudioSource dialougeSource;

    public bool IsQueueEmpty { get { return isQueueEmpty; } private set { isQueueEmpty = value; } }

    private bool isQueueEmpty = false;
    public static DialougeManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private static DialougeManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Singletons detected, deleting object. Please remove extra singleton from scene", this);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }


    /// <summary>
    /// Loads the dialouge clip at the end of the queue.
    /// </summary>
    /// <param name="clips">clips to load</param>
    /// <param name="clearQueue">should we clear the current queue? DOES NOT STOP THE CURRENT AUDIO</param>
    /// <param name="playOnLoad">plays as soon as the clips are ready, DOES NOT DO ANYTHING IF THE AUDIO IS ALREADY PLAYING</param>
    public void LoadDialougeClipsBehind(List<AudioClip> clips, bool clearQueue, bool playOnLoad)
    {
        if (clearQueue)
        {
            clipQueue.Clear();
        }

        clipQueue.AddRange(clips);

        if (playOnLoad && !dialougeSource.isPlaying)
        {
            if (currentPlayingCoroutine != null)
            {
                StopCoroutine(currentPlayingCoroutine);
            }

            currentPlayingCoroutine = StartCoroutine(PlayOnDialougeFinished());
        }

        isQueueEmpty = false;
    }

    private IEnumerator PlayOnDialougeFinished()
    {
        if (audioCount >= clipQueue.Count)
        {
            yield return new WaitUntil(() => { return !dialougeSource.isPlaying; });

            audioCount = 0;
            clipQueue.Clear();
            isQueueEmpty = true;

            yield break;
        }

        yield return new WaitUntil(() => { return !dialougeSource.isPlaying; });

        dialougeSource.clip = clipQueue[audioCount];
        dialougeSource.Play();

        audioCount++;

        currentPlayingCoroutine = StartCoroutine(PlayOnDialougeFinished());
    }

    public void LoadDialougeClipsAhead()
    {

    }

}
