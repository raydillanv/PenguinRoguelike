using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Dialogue IntroDialogue;
    public TextReader textReader;
    public GameObject IntroUI;
    public GameObject ShopCollider;

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
            
            yield return new WaitForSeconds(2f);
            if (textReader.NoMoreLines)
            {
                ShopCollider.SetActive(true);
                IntroUI.SetActive(false);
                yield break;
            }
            IntroDialogue.HandleDialogue();
        }
    }
}