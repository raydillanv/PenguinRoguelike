using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _NameText;
    [SerializeField] private TMP_Text _BodyText;

    public void OpenPanel()
    {
        _dialoguePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _dialoguePanel.SetActive(false);
    }

    public void UpdateDialogue(string name, string body)
    {
        _NameText.text = name;
        _BodyText.text = body;
    }

}
