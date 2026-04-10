using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Transform _player;
    public GameObject itemPrefab;
    public GameObject OpenChestGameRef;
    public GameObject ClosedChestRef;

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
        // Instantiate the item at the chest's position
        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // Swap chest visuals
        ClosedChestRef.SetActive(false);
        OpenChestGameRef.SetActive(true);

        print("Opened chest!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
}
