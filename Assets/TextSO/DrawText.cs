using System;
using UnityEngine;
using UnityEngine.UI;

public class DrawText : MonoBehaviour
{
    public Text TextField;

    [Space] public TextHandler defaultTextHandler;
    [Space] public Dropdown DropdownLanguage;

    private TextHandler _currentHandler;
    private Language _language = Language.rus;
    
    private void DrawNewText()
    {
        if (_currentHandler == null) return;
        var text = _currentHandler.text.Find(x => x.lang == _language);
        TextField.text = text.MainText;
    }
    
    public void NextDialogWindow()
    {
        ChangeHolder(defaultTextHandler.NextText);
    }

    private void Awake()
    {
        _currentHandler = defaultTextHandler;
        ChangeLanguage();
    }

    private void ChangeHolder(TextHandler handler)
    {
        _currentHandler = handler;
        DrawNewText();
    }

    

    public void ChangeLanguage()
    {
        _language = (Language) DropdownLanguage.value;
        DrawNewText();
    }


}