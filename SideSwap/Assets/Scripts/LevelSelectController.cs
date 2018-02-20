using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Hard-coded methods to avoid issues when loading levels from Level Selection scene

public class LevelSelectController : MonoBehaviour {

	public void loadLevelOne () {
        SceneManager.LoadScene("Level 1");
    }
    public void loadLevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void loadLevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void loadLevelFour()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void loadLevelFive()
    {
        SceneManager.LoadScene("Level 5");
    }
    public void loadLevelSix()
    {
        SceneManager.LoadScene("Level 6");
    }
    public void loadLevelSeven()
    {
        SceneManager.LoadScene("Level 7");
    }
    public void loadLevelEight()
    {
        SceneManager.LoadScene("Level 8");
    }
    public void backToMain()
    {
        SceneManager.LoadScene("Main");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //user can hit Esc to go back
        {
            SceneManager.LoadScene("Main");
        }
    }
}

