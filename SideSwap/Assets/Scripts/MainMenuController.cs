using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Methods for main menu buttons

public class MainMenuController : MonoBehaviour
{
    public string startingLevel;
    public string levelSelection;

    public void play()
    {
        SceneManager.LoadScene(startingLevel);
    }

    public void levelSelect()
    {
        SceneManager.LoadScene(levelSelection);
    }

    public void loadMain() //this is used by the "End" scene
    {
        SceneManager.LoadScene("Main");
    }

    public void quit()
    {
        Application.Quit();
    }
}
