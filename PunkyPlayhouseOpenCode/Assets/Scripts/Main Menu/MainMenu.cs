using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {

    public string newGameScene;

    public GameObject continuebutton;

    public string loadGameScene; 

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continuebutton.SetActive(true);
        }

        else
        {
            continuebutton.SetActive(false); 
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Continue()
    {

        SceneManager.LoadScene(loadGameScene); 

    }

    //makes a new game by loading first scene
    public void newGame()
    {
        SceneManager.LoadScene(newGameScene); 
    }

    //exits the game 
    public void Exit()
    {

        Application.Quit();

    }
}
