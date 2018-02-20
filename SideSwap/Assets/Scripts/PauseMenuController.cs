using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Handles pause menu buttons/fucntionality

public class PauseMenuController : MonoBehaviour
{
    public string mainMenu;
    public bool isPaused;
    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true); //dims screen and shows menu
            Time.timeScale = 0f; //freezes gameplay
            Cursor.visible = true;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }

        //Pause/Unpause using Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void resume()
    {
        isPaused = false;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
