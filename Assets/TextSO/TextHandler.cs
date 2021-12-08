using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextHandler", menuName = "ScriptableObjects/TextHandler", order = 1)]
public class TextHandler : ScriptableObject
{
    public string Name;
    public List<TextField> text;
    public TextHandler NextText;
}