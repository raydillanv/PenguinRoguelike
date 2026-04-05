using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currency;
    public int kills;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // subscirbes to OnCurrencyLooted
        DroppedLoot.OnCurrencyLooted += HandleOnCurrencyLooted;
    }

    private void OnDisable()
    {
        // unsubscirbes to OnCurrencyLooted
        DroppedLoot.OnCurrencyLooted -= HandleOnCurrencyLooted;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public int GetKillCount()
    {
        return kills;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void RemoveCurrency(int amount)
    {
        currency -= amount;
    }

    public void AddKillCount(int amount)
    {
        kills += amount;
    }

    // listeners for events
    private void HandleOnCurrencyLooted(int amount)
    {
        AddCurrency(amount);
        print(amount + " added to currency. Current currency: " +  currency);
    }

}