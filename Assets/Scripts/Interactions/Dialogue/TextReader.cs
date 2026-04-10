using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextReader : MonoBehaviour
{
    //public TextAsset textFileToParse;
    private DialogueController _dialogueController;

    public SFXPlayer player;
    public AudioClip passSound;

    private List<string> _lines = new List<string>();
    private int _currentLine = 0;
    private string _speaker;
    private string _body;
    public bool NoMoreLines = false;

    public void LoadText(TextAsset textAsset)
    {
        _lines = new List<string>(
            textAsset.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries)
        );
        _currentLine = 0;
    }

    public void parseText()
    {
        if (_currentLine >= _lines.Count)
        {
            Debug.Log("No more lines.");
            NoMoreLines = true;
            return;
        }

        player.PlaySFX(passSound);

        string line = _lines[_currentLine];
        string[] parts = line.Split(new[] { ':' }, 2);

        if (parts.Length == 2)
        {
            _speaker = parts[0].Trim();
            _body = parts[1].Trim();
        }

        _currentLine++;
    }

    public void PassText()
    {
        parseText();
        _dialogueController.UpdateDialogue(_speaker, _body);
    }

    public void RepeatText()
    {
        _currentLine = 0;
        NoMoreLines = false;
    }

    public void PassNewText(TextAsset textAsset)
    {
        LoadText(textAsset);
        NoMoreLines = false;
        PassText(); // immediately parse and display the first line
    }

    private void Start()
    {
        _dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        //LoadText(textFileToParse);
        //PassText();
    }
}