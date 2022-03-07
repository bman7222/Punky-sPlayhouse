using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using System.Diagnostics; 

public class QuestOBJActivator : MonoBehaviour {

    public GameObject objectToActivate;

    public string questToCheck;

    public bool activeIfComplete;

    private bool initialCheckDone; 

	// Use this for initialization
	void Start () {

    


    }
	
	// Update is called once per frame
	void Update () {

        //if an initial check isn't done then do one to check if the quest is complete or not
        if (!initialCheckDone)
        {
            initialCheckDone = true;

            checkCompletion();
        }

    }

    //checks if the quest is complete by using the quest manager's checkIfComplete method, th object is then set active based on if the object should or shouldn't be active upon completion
    public void checkCompletion ()
    {

        if (QuestManager.Instance.checkIfComplete(questToCheck))
        {
            objectToActivate.SetActive(activeIfComplete); 
        }

    }
}
