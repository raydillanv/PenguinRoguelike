using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settings;
    public GameObject shop;
    private bool isPaused;
    public GameObject controlPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (shop != null && shop.activeSelf)
            {
                shop.SetActive(false);
                Resume();
            } else if (settings.activeSelf)
            { 
                CloseSettings();
            }
            else
            { 
                if (isPaused) 
                { 
                    Resume();
                }
                else
                { 
                    Pause();
                }

            }

            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                if (!pauseMenu.activeSelf || !settings.activeSelf || (shop != null && !shop.activeSelf))
                {
                    if (controlPanel.activeSelf)
                    {
                        controlPanel.SetActive(false);
                        Time.timeScale = 1f;
                    }
                    else
                    {
                        controlPanel.SetActive(true);
                        Time.timeScale = 0f;
                    }
                }
            }
            
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    
    
    
    //Stages back to Title Screen
    public void quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Menu");
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        pauseMenu.SetActive(true);
    }
    
    public void restartLvl()
    {
        Time.timeScale = 1f;
        GameManager.instance.ResetLevel();
    }
}
