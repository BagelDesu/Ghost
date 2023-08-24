using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct MaterialColorPairs
{
    public CustomColor color;
    public Material material;
}

public class Screen : MonoBehaviour
{
    [SerializeField]
    private Material DefaultMaterial;
    [SerializeField]
    private MaterialColorPairs[] colourPairs;
    protected MeshRenderer rend;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void SwitchScreen(CustomColor color)
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

    public virtual void SwitchScreen(CustomDir dir) { }

    public virtual void ResetScreen()
    {
        rend.material = DefaultMaterial;
    }
}
