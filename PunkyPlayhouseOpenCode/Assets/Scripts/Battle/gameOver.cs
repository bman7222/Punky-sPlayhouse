using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class gameOver : MonoBehaviour {

    public string mainMenuScene, loadGameScene;

    public GameObject UICanvas;

    // Use this for initialization
    void Start () {

        //plays game over music
        AudioManager.Instance.playBGM(4);
 
        //turns off the player controller, game menu, and game mamanager
        PlayerController.Instance.gameObject.SetActive(false);
       // GameMenu.Instance.gameObject.SetActive(false);

     



    }
	
	// Update is called once per frame
	void Update () {
		



	}

    public void quitToMain()
    {
        //destorys all prefabs 
        Destroy(GameManager.Instance.gameObject);
        Destroy(PlayerController.Instance.gameObject);
        Destroy(GameMenu.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
     

        //goes to the main menu
        SceneManager.LoadScene(mainMenuScene);

    }


    public void loadLastSave()
    {

        //destorys all prefabs 
     Destroy(GameManager.Instance.gameObject);
   Destroy(PlayerController.Instance.gameObject);
       Destroy(GameMenu.Instance.gameObject);


        //goes to the scene of last saved data
        SceneManager.LoadScene(loadGameScene);

    }

} // end of script
