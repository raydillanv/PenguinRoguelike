using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int upgradeID;

    public int val;

    public int cost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if ((upgradeID == 0 && ItemPersistence.instance.upgradeBought1)
        //|| (upgradeID == 1 && ItemPersistence.instance.upgradeBought2) || 
        //(upgradeID == 2 && ItemPersistence.instance.upgradeBought3) || 
        //(upgradeID == 3 && ItemPersistence.instance.upgradeBought4))
        //gameObject.SetActive(false);
        
        /**
         * if another set of items is added to list, use this:
         * 
        if ((upgradeID == 4 && ItemPersistence.instance.upgradeBought5)
            || (upgradeID == 5 && ItemPersistence.instance.upgradeBought6) || 
            (upgradeID == 6 && ItemPersistence.instance.upgradeBought7) || 
            (upgradeID == 7 && ItemPersistence.instance.upgradeBought8))
            gameObject.SetActive(false);
         */
            
    }
    
    
    public void BuyHealth()
    {
        if (GameManager.instance.GetCurrency() >= cost)
        {
            GameManager.instance.AddToHealth(val);
            GameManager.instance.RemoveCurrency(cost);
            if (upgradeID == 0) ItemPersistence.instance.upgradeBought1 = true;
            if (upgradeID == 1) ItemPersistence.instance.upgradeBought2 = true;
            if (upgradeID == 2) ItemPersistence.instance.upgradeBought3 = true;
            if (upgradeID == 3) ItemPersistence.instance.upgradeBought4 = true;
            gameObject.SetActive(false); 
        }
    }
}
