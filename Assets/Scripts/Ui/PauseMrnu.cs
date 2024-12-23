using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMrnu : MonoBehaviour
{
    public GameObject pauseMenuui;
    private bool isPause = false;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

   public void Pause()
    {
        isPause = true;
        Time.timeScale = 0f;
        pauseMenuui.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

     public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        pauseMenuui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMianMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
        Application.Quit(); 
    }

}
