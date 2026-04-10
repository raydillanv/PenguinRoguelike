using UnityEngine;

public class ItemPersistence : MonoBehaviour
{
    public static ItemPersistence instance;

    public bool upgradeBought1;

    public bool upgradeBought2;
    public bool upgradeBought3;
    public bool upgradeBought4;
    
    /**
     * if an additional row is added to shop, use this
     * 
    public bool upgradeBought5;
    public bool upgradeBought6;
    public bool upgradeBought7;
    public bool upgradeBought8;
    */
    
    
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
    }


    
}
