using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct MaterialColorPairs
{
    public CustomColor color;
    public Material material;
}

public class ColourScreen : Screen
{
    [SerializeField]
    private Material DefaultMaterial;
    [SerializeField]
    private MaterialColorPairs[] colourPairs;

    protected override void Start()
    {
        base.Start();
        puzzle.colorScreens.Add(this);
    }

    public override void SwitchScreen(CustomColor color)
    {
        foreach (var item in colourPairs)
        {
            if (item.color == color)
            {
                rend.material = item.material;
                break;
            }
        }
    }

    public override void SwitchScreen()
    {
    }

    public override void SwitchScreen(CustomDir dir) 
    {
    }

    public override void ResetScreen()
    {
        rend.material = DefaultMaterial;
    }
}
