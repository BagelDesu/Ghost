using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    protected MeshRenderer rend;
    protected ColorPuzzle puzzle;

    protected virtual void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }
    protected virtual void Start()
    {
        puzzle = FindObjectOfType<ColorPuzzle>();
    }

    public abstract void SwitchScreen();
    public abstract void SwitchScreen(CustomColor color);
    public abstract void SwitchScreen(CustomDir dir);
    public abstract void ResetScreen();
}
