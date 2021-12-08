using UnityEngine;

[CreateAssetMenu(fileName = "TextField", menuName = "ScriptableObjects/TextField", order = 1)]
public class TextField : ScriptableObject
{
    [SerializeField]
    public Language lang;
    [TextArea (10,20)]
    public string MainText;
    [TextArea (5,10)]
    public string ExternalText;

    public string GetFullFieldText()
    {
        string fullText = MainText + " " + ExternalText;
        return fullText;
    }
}

public enum Language
{
    rus,
    eng
}