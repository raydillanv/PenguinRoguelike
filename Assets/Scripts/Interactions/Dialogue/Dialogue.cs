using UnityEngine;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    private DialogueController _dialogueController;
    private TextReader _textReaderRef;
    public TextAsset textFileToParse;
    public bool Repeatable = false;

    public bool GiveRandomText = false;

    public List<TextAsset> RandomTexts = new List<TextAsset>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        _textReaderRef = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<TextReader>();
        _textReaderRef.LoadText(textFileToParse);
        if (GiveRandomText)
        {
            _textReaderRef.LoadText(GetRandomText());
        }
    }

    public void HandleDialogue()
    {
        
        _dialogueController.OpenPanel();
        _textReaderRef.PassText();


        if (_textReaderRef.NoMoreLines)
        {
            
            _dialogueController.ClosePanel();

            if (GiveRandomText && Repeatable)
            {
                _textReaderRef.LoadText(GetRandomText());
                _textReaderRef.RepeatText();
                return;
            }

            if (Repeatable)
            {
                _textReaderRef.RepeatText();
            }
        }
    }

    public TextAsset GetRandomText()
    {
        if (RandomTexts.Count != 0){
            int length = RandomTexts.Count;
            int choice = Random.Range(0, length);
            return RandomTexts[choice];
        } else
        {
            print("Random is enabled but there are no text files in your list! Defaulting to original text.");
            return textFileToParse;
        }
        
    }

}
