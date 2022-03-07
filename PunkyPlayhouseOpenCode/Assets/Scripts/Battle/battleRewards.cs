using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI; 

public class battleRewards : MonoBehaviour {

    public static battleRewards instance;

    public Text expText, itemText;

    public GameObject rewardScreen,  closeButton; 

public string[] rewardItems;

    public int expEarned;


    // Use this for initialization
    void Start () {

        instance = this;

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Y)){
            openRewardScreen(10, new string[] { "c", "d" });
        }


    }

    //opens the reward screen
    public void openRewardScreen(int exp, string[] rewards)
    {
        //sets exp earned to exp
        expEarned = exp;

        //sets reward items to reward
        rewardItems = rewards;

        //set the xp text to the exp earned
        expText.text = expEarned.ToString() ; 

        //sets item text to blank
        itemText.text = "";

        //for every reward, add the item's name then do a new line
        for (int i = 0; i < rewardItems.Length; i++){

            itemText.text += rewards[i] +"\n";

        }
         
        //set the reward screen to active
        rewardScreen.SetActive(true);

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(closeButton);
    }

    //closes the reward screen
    public void closeRewardScreen()
    {

        //for every player
        for (int i = 0; i < GameManager.Instance.playerStats.Length; i++)
        {
            //if the player is active in the hierarchy
            if (GameManager.Instance.playerStats[i].gameObject.activeInHierarchy)
            {
                //add the earned exp to the palyers exp
                GameManager.Instance.playerStats[i].addEXP(expEarned);

            }

        }
        //for every item in the rewards
        for (int i = 0; i < rewardItems.Length; i++)
        {
            //add the item to the inventory
            GameManager.Instance.addItem(rewardItems[i]);


        }

        //turns of the reward screen 
        rewardScreen.SetActive(false);
       
        //set game manager instance  battle active to false
        GameManager.Instance.battleActive = false;
    }



}// end of script
