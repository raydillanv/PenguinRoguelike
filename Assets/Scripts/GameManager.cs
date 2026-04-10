using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public int currency;
    public int kills;
    public int Stars = 0;
    public int maxHealth = 100;
    public int health = 100;
    
    
    public bool stage1Done = false;
    public bool stage2Done = false;
    public bool stage3Done = false;
    public bool stage4Done = false;

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
            stage1Done = true;
        }
        else if (stage == "Stage 2")
        {
            stage2Done = true;
        }
        else if (stage == "Stage 3")
        {
            stage3Done = true;
        }
        else if (stage == "Stage 4")
        {
            stage4Done = true;
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

    public void AddStar()
    {
        Stars += 1;
    }

    public void AddKillCount(int amount)
    {
        kills += amount;
    }



    public void ResetHealth()
    {
        health = maxHealth;
    }

    // listeners for events
    private void HandleOnCurrencyLooted(int amount)
    {
        AddCurrency(amount);
        print(amount + " added to currency. Current currency: " +  currency);
    }

}