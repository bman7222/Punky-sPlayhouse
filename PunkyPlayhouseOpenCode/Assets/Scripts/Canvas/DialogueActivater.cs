using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivater : MonoBehaviour {

    public string[] lines;

    public bool canActivate;

    public bool isPerson = true;

    public bool markComplete;

    public string questToMark; 



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if in zone and presses button activate the managers show dialogue method
        if (canActivate && Input.GetButtonDown("Submit") && !DialogueManager2.Instance.dialogueBox.activeInHierarchy && GameManager.Instance.gameMenuOpen == false && Shop.Instance.shopIsOpen == false && BattleManager.Instance.battleActive==false) 
        {
            DialogueManager2.Instance.showDialogue(lines, isPerson);

            //if dialogue activates a quest then activate it after speaking to the person, the quest to be marked and whether it should be complete or not can be set here 
            DialogueManager2.Instance.shouldActivateQuestAtEnd(questToMark, markComplete); 
        }
		
	}

    //if player in zone 
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            canActivate = true; 
        }

    }

    //if player leaves zone
    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            canActivate = false;
        }

    }   


}
