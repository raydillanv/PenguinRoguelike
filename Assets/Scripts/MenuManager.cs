using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                pause();
                isPaused = true;
                Cursor.visible = true;
            }
            else
            {
                resume();
                isPaused = false;
                Cursor.visible = false;
            }
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    

    public void playButton()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void quit()
    {
        SceneManager.LoadScene("Start Menu");
        Time.timeScale = 1;
        Cursor.visible = true;
    }
    
    public void restartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
