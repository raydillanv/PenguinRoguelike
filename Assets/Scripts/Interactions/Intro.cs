using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Dialogue IntroDialogue;
    public TextReader textReader;
    public GameObject IntroUI;

    void Start()
    {
        StartCoroutine(DialogueCoroutine());
    }

    void Update()
    {
        
    }

    private IEnumerator DialogueCoroutine()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(3f);
            if (textReader.NoMoreLines)
            {
                IntroUI.SetActive(false);
                yield break;
            }
            IntroDialogue.HandleDialogue();
        }
    }
}