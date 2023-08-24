using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public Logger Instance
    {
        get
        {
            return Instance;
        }
        private set
        {
            Instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple Singletons detected, deleting object. Please remove extra singleton from scene", this);
            Destroy(this.gameObject);
            return;
        }  
    }

    public void Log()
    {

    }
}
