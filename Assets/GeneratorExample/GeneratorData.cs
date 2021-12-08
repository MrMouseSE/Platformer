using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GeneratorData", menuName = "ScriptableObjects/GeneratorData", order = 1)]
public class GeneratorData : ScriptableObject
{
    public int HeightField;
    public int WidthField;

    public GenerateObject obj;
}