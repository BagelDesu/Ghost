using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject subtitleGO;
    [SerializeField]
    private TextMeshProUGUI subtitleText;

    private string[] loadedTexts;
    private int currentText;

    [SerializeField]
    private bool shouldPauseGame;

    [SerializeField]
    private UnityEvent OnWindowOpen;

    public void LoadText(string[] textList)
    {
        loadedTexts = textList;
        currentText = 0;

        if (!subtitleGO.activeInHierarchy)
        {
            subtitleGO.SetActive(true);
        }
        AdvanceText();
        OnWindowOpen?.Invoke();
        if (shouldPauseGame)
        {
            GameManager.Instance.PauseGame();
        }
    }


    public void AdvanceText(InputAction.CallbackContext context)
    {
        // weird new input system way of handling "single presses"
        if (context.action.WasPressedThisFrame() && context.performed)
        {
            // If we're calling this func, without the subtitles window open, just return.
            // this could happen b/c we're just hooking this to the "space" button and forgetting about it.
            if (!subtitleGO.activeInHierarchy)
            {
                return;
            }

            if (loadedTexts.Length <= 0)
            {
                Debug.LogError("Missing subtitle text");
                return;
            }

            if (currentText < loadedTexts.Length)
            {
                subtitleText.text = loadedTexts[currentText];
            }
            else
            {
                if (shouldPauseGame)
                {
                    GameManager.Instance.ResumeGame();
                }
                ResetSubtitles();
                subtitleGO.SetActive(false);
            }
            currentText++;
            
        }

        if (context.action.WasReleasedThisFrame())
        {
            
        }
    }

    /// <summary>
    ///  Manual Advance Text
    /// </summary>
    public void AdvanceText()
    {
        // If we're calling this func, without the subtitles window open, just return.
        // this could happen b/c we're just hooking this to the "space" button and forgetting about it.
        if (!subtitleGO.activeInHierarchy)
        {
            return;
        }

        if (loadedTexts.Length <= 0)
        {
            Debug.LogError("Missing subtitle text");
            return;
        }

        if (currentText < loadedTexts.Length)
        {
            subtitleText.text = loadedTexts[currentText];
        }
        else
        {
            if (shouldPauseGame)
            {
                GameManager.Instance.ResumeGame();
            }
            ResetSubtitles();
            subtitleGO.SetActive(false);
        }
        currentText++;
    }

    public void ResetSubtitles()
    {
        loadedTexts = null;
        currentText = 0;
    }

}
