using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    [SerializeField]
    private string buttonValue;

    private Keypad keypad;

    private void Start()
    {
        keypad = GetComponentInParent<Keypad>();
    }

    public void SendButtonValue()
    {
        keypad.AppendValue(buttonValue);
    }
}
