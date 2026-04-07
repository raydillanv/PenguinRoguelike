using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currency;
    public int kills;
    public int Stars = 0;
    
    private bool _Stage1Done = false;
    private bool _Stage2Done = false;
    private bool _Stage3Done = false;
    private bool _Stage4Done = false;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
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

    public void VisitStage(string stage)
    {
        if (stage == "Stage 1")
        {
            _Stage1Done = true;
        }
        else if (stage == "Stage 2")
        {
            _Stage2Done = true;
        }
        else if (stage == "Stage 3")
        {
            _Stage3Done = true;
        }
        else if (stage == "Stage 4")
        {
            _Stage4Done = true;
        }
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