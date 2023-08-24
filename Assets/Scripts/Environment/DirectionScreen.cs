using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DirectionMaterialPairs
{
    public CustomDir direction;
    public Material material;
}

public class DirectionScreen : Screen
{
    [SerializeField]
    private DirectionMaterialPairs[] dirs;



    public override void SwitchScreen(CustomDir dir)
    {
        foreach (var item in dirs)
        {
            if (item.direction == dir)
            {
                rend.material = item.material;
                break;
            }
        }
    }
}
