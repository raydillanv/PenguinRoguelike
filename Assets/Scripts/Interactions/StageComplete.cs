using UnityEngine;
using UnityEngine.SceneManagement;

public class StageComplete : MonoBehaviour, IInteractable
{
    private Transform _player;
    private GameManager _gameManager;
    
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
        _gameManager.VisitStage(sceneName);
        _gameManager.AddStar();
    }
}