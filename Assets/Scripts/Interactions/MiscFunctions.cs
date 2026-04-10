using UnityEngine;
using UnityEngine.SceneManagement;
public class MiscFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Home");
        GameManager.instance.RefreshPlayerReference();
    }

    public void ResumeOnExitUI()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
