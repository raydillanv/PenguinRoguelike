using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Transform _player;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        OpenChest();
    }

    public void OpenChest()
    {
        // opening chest
        print("Opened chest!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
