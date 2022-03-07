using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadingScene : MonoBehaviour {

    public float waitToLoad;
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if wait to load is greater than zero than load the scene, save data, and all quest data
        waitToLoad -= Time.deltaTime;

        if (waitToLoad < 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

            GameManager.Instance.loadData();
            QuestManager.Instance.loadQuestData();
        }

     

	}


}
