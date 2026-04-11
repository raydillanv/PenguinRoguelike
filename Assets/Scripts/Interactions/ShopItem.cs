using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int upgradeID;

    public int val;

    public int cost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ((upgradeID == 0 && PurchaseSingleton.instance.upgradeBought1)
        || (upgradeID == 1 && PurchaseSingleton.instance.upgradeBought2) ||
        (upgradeID == 2 && PurchaseSingleton.instance.upgradeBought3) ||
        (upgradeID == 3 && PurchaseSingleton.instance.upgradeBought4))
        gameObject.SetActive(false);

        if ((upgradeID == 4 && PurchaseSingleton.instance.upgradeBought5)
            || (upgradeID == 5 && PurchaseSingleton.instance.upgradeBought6) ||
            (upgradeID == 6 && PurchaseSingleton.instance.upgradeBought7) ||
            (upgradeID == 7 && PurchaseSingleton.instance.upgradeBought8))
            gameObject.SetActive(false);

    }
    
    
    public void BuyHealth()
    {
        if (GameManager.instance.GetCurrency() >= cost)
        {
            GameManager.instance.AddToHealth(val);
            GameManager.instance.RemoveCurrency(cost);
            if (upgradeID == 0) PurchaseSingleton.instance.upgradeBought1 = true;
            if (upgradeID == 1) PurchaseSingleton.instance.upgradeBought2 = true;
            if (upgradeID == 2) PurchaseSingleton.instance.upgradeBought3 = true;
            if (upgradeID == 3) PurchaseSingleton.instance.upgradeBought4 = true;
            gameObject.SetActive(false); 
        }
    }
    
    public void BuySpeed()
    {
        if (GameManager.instance.GetCurrency() >= cost)
        {
            GameManager.instance.AddToMoveSpeed(val);
            GameManager.instance.RemoveCurrency(cost);
            if (upgradeID == 4) PurchaseSingleton.instance.upgradeBought5 = true;
            if (upgradeID == 5) PurchaseSingleton.instance.upgradeBought6 = true;
            if (upgradeID == 6) PurchaseSingleton.instance.upgradeBought7 = true;
            if (upgradeID == 7) PurchaseSingleton.instance.upgradeBought8 = true;
            gameObject.SetActive(false);
        }
    }
}
