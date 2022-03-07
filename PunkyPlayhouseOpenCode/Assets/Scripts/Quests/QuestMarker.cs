using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{

    public string questToMark;

    public bool markComplete;

    public bool markOnEnter;

    private bool canMark;

    public bool deactivateOnMark; 

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        // if the quest is markable and the button is clicked, then deactivate canmark and do the markquest method
        if(canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;

            markQuest();
        }


    }

    //marks a quest if it is complete (markcomplete=true) otherwise it marks it incomplete, also activates or deactivates a quest object 
    public void markQuest()
    {

        if (markComplete)
        {
            QuestManager.Instance.markQuestComplete(questToMark); 
        }

        else
        {
            QuestManager.Instance.markQuestIncomplete(questToMark);
        }

        gameObject.SetActive(!deactivateOnMark);

    }

    //if the player enters the zone the trigger then canmark is set true 
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            canMark = true;
        }

        if (markOnEnter == true)
        {
            markQuest();
        }

    }

    //if a player exits the zone then the quest is marked if markonenter is true otherwise it sets canmark to false so that the quest can't be marked
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

  
         canMark = false;
            

        }

    }



} //end of script 