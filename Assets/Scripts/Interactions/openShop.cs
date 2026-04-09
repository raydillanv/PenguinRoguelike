using UnityEngine;

public class openShop : MonoBehaviour
{
    private Transform _player;
    private GameObject _shop; 

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        shop();
    }

    public void shop()
    {
        _shop.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _shop = GameObject.FindGameObjectWithTag("Shop");
    }
}
