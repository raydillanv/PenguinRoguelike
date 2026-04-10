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
    private float _currentHealth;
    private float _currentMana;
    private string _currentStage;
    
    // Autograbs on sceneload
    public GameObject player;
    public PlayerBehavior playerScript;
    
    
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
        _currentHealth = maxHealth;
        _currentMana = maxMana;
        _currentStage = SceneManager.GetActiveScene().name;
        RefreshPlayerReference();
    }

    public void FixedUpdate()
    {
        if (_currentMana < maxMana)
            _currentMana = Mathf.Min(_currentMana + Mathf.Ceil(manaRegen), maxMana);
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
    }
    
    public void takeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) ResetLevel();
    }

    public void RestoreHealth(float value)
    {
        _currentHealth = Mathf.Min(_currentHealth + Mathf.Ceil(value), GameManager.instance.maxHealth);
    }
    
    public void ResetCharacter() { _currentHealth = maxHealth; _currentMana = maxMana; }

    public void ResetLevel()
    {
        ResetCharacter();
        SceneManager.LoadScene(_currentStage);
        RefreshPlayerReference();
    }

    // listeners for events
    private void HandleOnCurrencyLooted(int amount)
    {
        AddCurrency(amount);
        print(amount + " added to currency. Current currency: " +  currency);
    }

    public void RefreshPlayerReference()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerBehavior>();
    }
}