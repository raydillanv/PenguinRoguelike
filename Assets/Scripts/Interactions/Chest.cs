using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Transform _player;
    public GameObject itemPrefab;
    public GameObject OpenChestGameRef;
    public GameObject ClosedChestRef;
    public int Amount = 1;

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
        // Instantiate items at the chest's position
        for (int i = 0; i < Amount; i++)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        // Swap chest visuals
        ClosedChestRef.SetActive(false);
        OpenChestGameRef.SetActive(true);

        print("Opened chest!");
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}