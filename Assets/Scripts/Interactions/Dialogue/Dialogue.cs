using UnityEngine;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    private DialogueController _dialogueController;
    private TextReader _textReaderRef;
    public TextAsset textFileToParse;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        _textReaderRef = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<TextReader>();
        _textReaderRef.LoadText(textFileToParse);
    }

    public void HandleDialogue()
    {
        _dialogueController.OpenPanel();
        _textReaderRef.PassText();
        if (_textReaderRef.NoMoreLines)
        {
            _dialogueController.ClosePanel();
        }
    }

}
