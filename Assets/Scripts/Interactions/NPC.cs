using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public bool TalkedAlready {  get; private set; }
    [SerializeField] private DialogueController _dialogueController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TalkedAlready = false;
        //_dialogueController = FindAnyObjectByType<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        //f (!CanInteract()) return;
        // interact with npc
        print("Interacted with NPC.");
        _dialogueController.OpenPanel();
    }

}
