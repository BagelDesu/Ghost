
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSubtitlePopup : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string[] subtitleRequest;

    [SerializeField]
    private SubtitlesManager subtitlesManager;

    public void SendRequest()
    {
        subtitlesManager.LoadText(subtitleRequest);
    }
}
