using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void UpdateText(int timer)
    {
        text.text = timer.ToString();
    }

    public void ResetText()
    {
        text.text = "0";
    }
}
