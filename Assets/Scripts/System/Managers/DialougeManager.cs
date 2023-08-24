using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialougeManager : MonoBehaviour
{
    private bool playingCheck;
    private List<AudioClip> clipQueue = new List<AudioClip>();

    private AudioSource dialougeSource;
    private UnityEvent OnDialougeFinished;

    private void Awake()
    {
        OnDialougeFinished.AddListener(PlayClip);
    }

    private void Start()
    {
        dialougeSource = GetComponent<AudioSource>();
        playingCheck = false;
    }

    private void Update()
    {
        if (!dialougeSource.isPlaying && playingCheck)
        {
            OnDialougeFinished?.Invoke();
            playingCheck = false;
        }
    }

    public void QueueDialougeClip(bool jumpQueue, AudioClip clip)
    {
        if (jumpQueue)
        {
            clipQueue.Insert(0, clip);
        }
        clipQueue.Add(clip);
    }

    private void PlayClip()
    {
        if (clipQueue.Count > 0)
        {
            dialougeSource.clip = clipQueue[clipQueue.Count - 1];
        }
        
        clipQueue.RemoveAt(0);
        dialougeSource.Play();
        playingCheck = true;
    }

    public void PlayClip(AudioClip clip)
    {
        dialougeSource.Stop();
        dialougeSource.clip = clip;
        dialougeSource.Play();
        playingCheck = true;
    }
}
