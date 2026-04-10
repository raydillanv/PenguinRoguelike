using UnityEngine;
using TMPro;

public class openShop : MonoBehaviour, IInteractable
{
    private Transform _player;
    public GameObject _shop;
    public TMP_Text fishCount;

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
        Time.timeScale = 0;
        Cursor.visible = true;
        fishCount.SetText(GameManager.instance.GetCurrency().ToString());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //_shop = GameObject.FindGameObjectWithTag("Shop");
    }
}
