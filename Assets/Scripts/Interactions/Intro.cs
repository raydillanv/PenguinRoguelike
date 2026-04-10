using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Dialogue IntroDialogue;

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
            
            yield return new WaitForSeconds(5f);
            IntroDialogue.HandleDialogue();
        }
    }
}