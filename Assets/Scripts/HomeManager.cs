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

    public GameObject BossDoorNotReady;
    public GameObject BossDoorReady;
    
    // A script that sets the home scene up on start based on the bools inside the game manager.
    
    
    
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetDoors();
        SetNPCs();
        print(0);
    }

    private void SetDoors()
    {
        if (_gameManager.stage1Done == true)
        {
            Stage1Door.SetActive(false);
            print("Stage 1 Done");
        }

        if (_gameManager.stage2Done == true)
        {
            Stage2Door.SetActive(false);
        }

        if (_gameManager.stage3Done == true)
        {
            Stage3Door.SetActive(false);
        }

        if (_gameManager.stage4Done == true)
        {
            Stage4Door.SetActive(false);
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
    
}
