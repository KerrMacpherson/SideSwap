using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //GameManager must be a singleton

    private int level = 0;
    private Text levelText;

	void Awake () {
        //make sure there's only one instance of GameManager
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); //don't try to load new GameManager over the existing one
        InitGame();
	}

    //Called when a scene is loaded
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        level++;
        InitGame();
    }
    //listen for scene change event
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    //stop listening
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void InitGame() //initialises game, displays UI
    {
        Scene scene = SceneManager.GetActiveScene();
        if ((scene.name != "Main") && (scene.name != "Level Select") && (scene.name != "End")) //scene is a playable level
        {
            Cursor.visible = false;
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            string sceneName = SceneManager.GetActiveScene().name;
            levelText.text = sceneName;
        } else {
            Cursor.visible = true;
        }
    }

    public void loadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "End")
        {
            //Reference: http://answers.unity3d.com/questions/1169114/how-to-load-next-scene-in-unity-5.html
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
