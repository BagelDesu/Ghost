using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;

    public static GameManager Instance
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

    private static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple Singletons detected, deleting object. Please remove extra singleton from scene", this);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }

    public void StartGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void StartEnd(int endCode)
    {

    }
}
