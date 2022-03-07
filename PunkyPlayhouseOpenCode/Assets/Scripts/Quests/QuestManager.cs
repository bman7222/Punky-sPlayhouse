using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public string[] questMarkerName;

    public bool[] questMarkerComplete;

    public static QuestManager Instance;

    // Use this for initialization
    void Start() {
        //set the quest manager
        Instance = this;

        //make the quest marker complete equal to the length of the current quests 
        questMarkerComplete = new bool[questMarkerName.Length];

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.H)){
            saveQuestData(); 
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            loadQuestData();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(checkIfComplete("a"));
           
        }


    }

    //gets the correct quest number for the quest in the array
    public int getQuestNumber(string questToFind)
    {
        //searches the marker names for the name of the quest to find, if it fidns it it returns the quest to find's number, otherwise i returns 0
        for (int i = 0; i < questMarkerName.Length; i++)
        {
            if (questMarkerName[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogError("Quest " + questToFind + " doesn't exist");
        return 0;
    }

    //check if the quest is complete 
    public bool checkIfComplete(string questToCheck)
    {
        //if the get quest number method works then return the quest marker complete at the position of the quest's number (the quest to check's number) else return false 
        if (getQuestNumber(questToCheck) != 0)
        {
            return questMarkerComplete[getQuestNumber(questToCheck)];
        }

        return false;
    }

    //marks as complete by going through the questmarkercomplete array and getting the quest number of the quest to mark and setting it to true 
    public void markQuestComplete(string questToMark)
    {

        questMarkerComplete[getQuestNumber(questToMark)] = true;

        updateLocalQuestObjects();


    }

    //marks as incomplete
    public void markQuestIncomplete(string questToMark)
    {

        questMarkerComplete[getQuestNumber(questToMark)] = false;

        updateLocalQuestObjects();


    }

    //activates or deactivates quest objects once a quest is marked incomplete or complete 
    public void updateLocalQuestObjects()
    {

        QuestOBJActivator[] questObjects = FindObjectsOfType<QuestOBJActivator>();

        if (questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].checkCompletion();
            }
        }

    }

    //cycles through all quests to find which ones are complete 
    public void saveQuestData()
    {
        //cycles through all quests and finds if a quest is complete if it is then it goes to player prefs of the quest to 1 otherwise set it to 0
        for(int i=0; i<questMarkerName.Length; i++)
        {
            if (questMarkerComplete[i])
            {
                PlayerPrefs.SetInt("Quest Marker_" + questMarkerName[i], 1);
            }

            else
            {
                PlayerPrefs.SetInt("Quest Marker_" + questMarkerName[i], 0);
            }

        } //end of for loop

    }

    //laods the data of a quest to see if its complete or not 
    public void loadQuestData()
    {

        //loop through all quests, if the quest has the  Quest Marker_ before hand (which it should if saved" then set its value according to what it shoudl be set to when saving quests, otherwise it is set to 0 automatically 
        for (int i = 0; i < questMarkerName.Length; i++)
        {
            int valueToSet = 0; 

            if(PlayerPrefs.HasKey("Quest Marker_" + questMarkerName[i]))
            {
                valueToSet = PlayerPrefs.GetInt("Quest Marker_" + questMarkerName[i]); 
            }

            if (valueToSet == 0)
            {
                questMarkerComplete[i] = false; 
            }

            else
            {
                questMarkerComplete[i] = true; 
            }

        }

    }



} // end of script
