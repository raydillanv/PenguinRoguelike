using UnityEngine;

public class PurchaseScript : MonoBehaviour
{
    public int cost;
    public int value;
    

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BuyHealth()
    {
        if (GameManager.instance.GetCurrency() >= cost)
        {
            GameManager.instance.RemoveCurrency(cost);
            GameManager.instance.addHealth(value);
        }
    }
    
    
}
