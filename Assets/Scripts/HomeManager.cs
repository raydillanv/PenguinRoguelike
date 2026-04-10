using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private GameManager _gameManager;

    public GameObject Stage1Door;
    public GameObject Stage2Door;
    public GameObject Stage3Door;
    public GameObject Stage4Door;

    public GameObject Stage1NPCs;
    public GameObject Stage2NPCs;
    public GameObject Stage3NPCs;
    public GameObject Stage4NPCs;

    public GameObject Stage1Star;
    public GameObject Stage2Star;
    public GameObject Stage3Star;
    public GameObject Stage4Star;

    public GameObject BossDoorNotReady;
    public GameObject BossDoorReady;
    public GameObject BossDoorAnimation;

    public GameObject IntroScene;
    public GameObject ShopCollider;

    // A script that sets the home scene up on start based on the bools inside the game manager.



    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetDoors();
        SetNPCs();
        SetStars();
        print(0);
    }

    private void SetDoors()
    {
        if (_gameManager.stage1Done == true)
        {
            Stage1Door.SetActive(false);
            //print("Stage 1 Done");
            IntroScene.SetActive(false);
            ShopCollider.SetActive(true);
        }

        if (_gameManager.stage2Done == true)
        {
            Stage2Door.SetActive(false);
            IntroScene.SetActive(false);
            ShopCollider.SetActive(true);
        }

        if (_gameManager.stage3Done == true)
        {
            Stage3Door.SetActive(false);
            IntroScene.SetActive(false);
            ShopCollider.SetActive(true);
        }

        if (_gameManager.stage4Done == true)
        {
            Stage4Door.SetActive(false);
            IntroScene.SetActive(false);
            ShopCollider.SetActive(true);
        }

        if (_gameManager.Stars >= 4)
        {
            BossDoorNotReady.SetActive(false);
            BossDoorReady.SetActive(true);
            BossDoorAnimation.SetActive(true);
        }
    }

    private void SetNPCs()
    {
        if (_gameManager.stage1Done == true)
        {
            Stage1NPCs.SetActive(true);
        }

        if (_gameManager.stage2Done == true)
        {
            Stage2NPCs.SetActive(true);
        }

        if (_gameManager.stage3Done == true)
        {
            Stage3NPCs.SetActive(true);
        }

        if (_gameManager.stage4Done == true)
        {
            Stage4NPCs.SetActive(true);
        }
    }

    private void SetStars()
    {
        if (_gameManager.stage1Done == true)
        {
            Stage1Star.SetActive(true);
        }

        if (_gameManager.stage2Done == true)
        {
            Stage2Star.SetActive(true);
        }

        if (_gameManager.stage3Done == true)
        {
            Stage3Star.SetActive(true);
        }

        if (_gameManager.stage4Done == true)
        {
            Stage4Star.SetActive(true);
        }
    }
    
}
