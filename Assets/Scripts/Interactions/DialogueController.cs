using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;

    public void OpenPanel()
    {
        _dialoguePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _dialoguePanel.SetActive(false);
    }

}
