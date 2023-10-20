using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    [SerializeField]
    private TheaterSetSO[] theaterSet;

    public TheaterSetSO[] TheaterSet { get { return theaterSet; } private set{ theaterSet = value; }}
}
