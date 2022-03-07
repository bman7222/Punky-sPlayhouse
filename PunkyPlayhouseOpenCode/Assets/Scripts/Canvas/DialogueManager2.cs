using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager2 : MonoBehaviour {

    public TextMeshProUGUI dialogueText;
    public Text nameText;
    public GameObject dialogueBox;
    public GameObject nameBox;

    public string[] dialogueLines;

    public int currentLine;

    public static DialogueManager2 Instance;

    private bool juststarted;

    private string questToMark;

    private bool markQuestComplete, shouldMarkQuest; 

	// Use this for initialization
	void Start () {

        Instance = this;

       


	}
	
	// Update is called once per frame
	void Update () {

        //if dialogue box is active

        if (dialogueBox.activeInHierarchy)
        {
            //if the button assigned to fire1 is pressed
            if (Input.GetButtonUp("Submit"))
            {

                if (!juststarted)
                {
                    //go to next line
                    currentLine++;

                    //if the next line doesn't exist
                    if (currentLine >= dialogueLines.Length)
                    {
                        //deactivate dialogue box
                        dialogueBox.SetActive(false);

                        // game manaer says the player can move again
                        GameManager.Instance.dialogueActive = false;

                        //if whiel talking a wuest should be marked then mark unmark it, if the quest should be marked as compleye then access the quest manager to compelte it otherwise mark it as incomplete
                        if (shouldMarkQuest == true)
                        {
                           
                            shouldMarkQuest = false;

                            if (markQuestComplete == true)
                            {
                                QuestManager.Instance.markQuestComplete(questToMark); 
                            }

                            else
                            {
                                QuestManager.Instance.markQuestIncomplete(questToMark);
                            }

                        }
                    }

                    //otherwise go to next line
                    else
                    {

                        checkIfName(); 

                        //go to next line
                        dialogueText.text = dialogueLines[currentLine];
                    }
                }

                else
                {
                    //check if name switch
                    checkIfName(); 

                    //delay button presses
                    juststarted = false; 
                }
                
            }
        }


	}

    public void showDialogue( string [] newlines, bool isPerson)
    {

        //recieve dialogue
        dialogueLines = newlines;

        //set the current line to 0 
        currentLine = 0;

        //check if name switch
        checkIfName();

        //use text 
        dialogueText.text = dialogueLines[currentLine];

        //set the active part
        dialogueBox.SetActive(true);

        //make clickign work 
        juststarted = true;

        //check if they are a person
        nameBox.SetActive(isPerson);

        //game manager says player cannot move because dialogue is active
        GameManager.Instance.dialogueActive = true; 
    }


    //checks if the lie starts with n- if so then change who is in the anme box
    public void checkIfName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
           nameText.text = dialogueLines[currentLine].Replace("n-", ""); 

            currentLine++; 
        }
    }

    //marks a quest, sets m
    public void shouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true; 
    }

}
