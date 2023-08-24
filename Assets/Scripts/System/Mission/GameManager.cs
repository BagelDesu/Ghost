using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public void StartGame()
    {

    }

    public void ExitGame()
    {

    }

    private void StartEnd(int endCode)
    {

    }
}
