using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider; 
    public TMP_Text healthText;
    public Slider manaSlider;
    public TMP_Text manaText;
    
    private string healthString => $"{GameManager.instance.health:F2}/{GameManager.instance.maxHealth}";
    private string manaString => $"{GameManager.instance.mana:F2}/{GameManager.instance.maxMana:F2}";
    public void RefreshValues()
    {
        healthSlider.value = GameManager.instance.health;
        manaSlider.value = GameManager.instance.mana;
        healthSlider.maxValue = GameManager.instance.maxHealth;
        manaSlider.maxValue = GameManager.instance.maxMana;
        healthText.text = healthString;
        manaText.text = manaString;
    }
}