using System;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    
    public int currency;
    public int kills;
    public int Stars = 0;
    public float maxMana = 100f;
    public float maxHealth = 100f;
    public float manaRegen = 5f;
    public float moveSpeed = 5f;
    
    // Keep track of currents
    public float health;
    public float mana;
    private string _currentStage;
    
    // Autograbs on sceneload
    public GameObject player;
    public PlayerBehavior playerScript;
    public UIManager uiManager;
    
    public bool stage1Done = false;
    public bool stage2Done = false;
    public bool stage3Done = false;
    public bool stage4Done = false;

    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        health = maxHealth;
        mana = maxMana;
        _currentStage = SceneManager.GetActiveScene().name;
        RefreshPlayerReference();
        uiManager.RefreshValues();
    }

    public void FixedUpdate()
    {
        if (mana < maxMana)
        {
            mana = Mathf.Min(mana + Mathf.Ceil(manaRegen), maxMana);
            uiManager.RefreshValues();
        }

    }

    private void OnEnable()
    {
        // subscirbes to OnCurrencyLooted
        DroppedLoot.OnCurrencyLooted += HandleOnCurrencyLooted;
        DroppedLootHeal.OnHealLooted += HandleOnHealLooted;
        DroppedLootMana.OnManaLooted += HandleOnManaLooted;
        
    }

    private void OnDisable()
    {
        // unsubscirbes to OnCurrencyLooted
        DroppedLoot.OnCurrencyLooted -= HandleOnCurrencyLooted;
        DroppedLootHeal.OnHealLooted -= HandleOnHealLooted;
        DroppedLootMana.OnManaLooted -= HandleOnManaLooted;
    }

    public void VisitStage(string stage)
    {
        if (stage == "Stage 1") {
            stage1Done = true;
        } else if (stage == "Stage 2") {
            stage2Done = true; 
        } else if (stage == "Stage 3") {
            stage3Done = true;
        } else if (stage == "Stage 4") {
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
    public void AddToHealth(float value)
    {
        maxHealth += value;
        uiManager.RefreshValues();
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) ResetLevel();
        uiManager.RefreshValues();
    }

    public bool ReduceMana(float value)
    {
        mana -= value;
        if (mana <= 0)
        {
            mana = 0;
            return false;
        }
        
        uiManager.RefreshValues();
        return true;
    }

    public void RestoreHealth(float value)
    {
        health = Mathf.Min(health + Mathf.Ceil(value), GameManager.instance.maxHealth);
        uiManager.RefreshValues();
    }

    public void ResetCharacter() { health = maxHealth; mana = maxMana; }

    public void ResetLevel()
    {
        ResetCharacter();
        SceneManager.LoadScene(_currentStage);
        RefreshPlayerReference();
        uiManager.RefreshValues();
    }

    // listeners for events
    private void HandleOnCurrencyLooted(int amount)
    {
        AddCurrency(amount);
        print(amount + " added to currency. Current currency: " +  currency);
    }

    private void HandleOnHealLooted(int amount)
    {
        RestoreHealth(amount);
        uiManager.RefreshValues();
    }

    private void HandleOnManaLooted(int amount)
    {
        print("adds mana when thats implemented.");
    }

    public void RefreshPlayerReference()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerBehavior>();
        uiManager = FindAnyObjectByType<Canvas>()?.GetComponent<UIManager>();
    }
}