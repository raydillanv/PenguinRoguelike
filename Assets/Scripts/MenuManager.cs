using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    private GameObject pauseMenu;

    private bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
        pauseMenu = GameObject.FindGameObjectWithTag("pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            pause();
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
    

    public void playButton()
    {
        SceneManager.LoadScene("Home");
        resume();
    }

    public void quit()
    {
        SceneManager.LoadScene("Start Menu");
        resume();
    }
    
    public void restartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        resume();
    }
}
