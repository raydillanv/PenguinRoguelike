using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDoor : MonoBehaviour, IInteractable
{
    private Transform _player;
    
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }
}
