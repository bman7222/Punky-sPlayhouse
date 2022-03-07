using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen;

    public bool dialogueActive, battleActive;

    public bool fadingBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfHeld;
    public Item[] referrenceItems;

    public int currentGold;

    public bool shopActive; 

    // Use this for initialization
    void Start()
    {

        //make this the game manager
        Instance = this;

        //DontDestroyOnLoad(gameObject);

        sortItems();

  

    }

    // Update is called once per frame
    void Update()
    {

        //if the menu is open or dialogue active or you are fading between areas
        if (gameMenuOpen || dialogueActive || fadingBetweenAreas || shopActive || battleActive)
        {
            //make player unable to move
            PlayerController.Instance.canMove = false;

        }

        //otheriwse make moving  availible
        else
        {
            PlayerController.Instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            addItem("HP Potion");
            addItem("blaval"); 
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            removeItem("MP Potion");
            removeItem("blaval");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            saveData();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            loadData();
        }

    }

    //go through items list and find a certain item
    public Item getItemDetails(string itemToGrab)
    {
        //go through every item
        for (int i = 0; i < referrenceItems.Length; i++)
        {
            //if it is referrened
            if (referrenceItems[i].itemName == itemToGrab)
            {
                //return the item 
                return referrenceItems[i];
            }
        }

        return null;
    }

    public void sortItems()
    {
        bool itemAfterSpace = true;

        //make sure that all spaces are gotten rid of 
        while (itemAfterSpace)
        {
            //prevent infinity loop
            itemAfterSpace = false; 
            //go through all items
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {

                //if it is blank move to the next item
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfHeld[i] = numberOfHeld[i + 1];
                    numberOfHeld[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }

                } // end of if statement 
            }  //end of for loop

        } // end of while loop


    } //end of public void

    public void addItem(string itemToAdd)
    {
        //item position and if there is open space
        int newItemPosition = 0;
        bool foundSpace = false; 

        //cycle through the item list and if an empty item or matching item is found add it to the empty slot or already existing item stack
        for(int i=0; i<itemsHeld.Length; i++)
        {
            if(itemsHeld[i]=="" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;  
            }
        }

        //when space is found activate this section
        if (foundSpace)
        {
            //sees if item exists in game or not
            bool itemExists = false;  

            //loop through all referrenced item to find existing info
            for (int i = 0; i < referrenceItems.Length; i++)
            {
                //id the item to adds name is in the list
                if(referrenceItems[i].itemName==itemToAdd)
                {
                    //set item exists to true 
                    itemExists = true;

                    //stop i from searchign anymore
                    i = referrenceItems.Length;
                }
            }

            //wehn it is found that the item does exist 
            if (itemExists)
            {
                //add the new item in the slot and add it to the stack
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfHeld[newItemPosition]++;
            }

            //if not in refferenced say does not exist
            else
            {
                Debug.LogError(itemToAdd + " does not exist"); 
            }
        } // end of found space

        //set item buttons
        GameMenu.Instance.showItems();

    } //end of void add

    public void  removeItem (string itemToRemove)
    {

        //variabels for fidnign item and its position
        bool foundItem = false;
        int itemPosition = 0;

        //loop through all items to find the item to remove
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            //whewn foun set foundItem to true, the position to i, and i to the end of the list to stop it from continuing 
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;
            }
        }

        //if the item is found make the number of held go down and if it goes to or below zero then set the button to null
            if (foundItem)
            {
                numberOfHeld[itemPosition]--;

                if(numberOfHeld[itemPosition] <= 0)
                {
                    itemsHeld[itemPosition] = ""; 
                }

                //set item buttons
            GameMenu.Instance.showItems(); 

            }

            //if item isnt found set debug message
            else
            {
                Debug.LogError("Couldn't find " + itemToRemove);
            }
        

    } //end of void remove

    //saves the player's current position, current scene, character details (level,items,party members, etc) 
    public void saveData()
    {

        //SAVE PLAYER POSTION
        //save scene 
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);

        //save x pos
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.Instance.transform.position.x);

        //save y pos
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.Instance.transform.position.y);

        //save z pos
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.Instance.transform.position.z);

        //SAVE CHARACTER DETAILS/INFO

        //loops through al characters to see if they are active or not 
        for(int i=0; i<playerStats.Length; i++)
        {
            //if active then set to 1 otherwis set to 0 
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1); 
            }

            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }

            //saves the stats, weapons/armor , exp, and level
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentEXP", playerStats[i].currentEXP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_strength", playerStats[i].str);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_defense", playerStats[i].def);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_weapon", playerStats[i].wpn);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_armor", playerStats[i].armor);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_weaponName", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_armorName", playerStats[i].equippedArmor);

        } //for loop for player stats end 

        //STORE ITEMS

        //cycles through items and saves what is in each slot and how many there are
        for(int i=0; i<itemsHeld.Length; i++)
        {

            PlayerPrefs.SetString("itemInInventory" + i, itemsHeld[i]);

            PlayerPrefs.SetInt("itemAmount_"+i, numberOfHeld[i]); 

        }


    } // end of save data 

    //loads player position point and scene 
    public void loadData()
    {

        PlayerController.Instance.areaTransitionName = "";

        //load the saved x,y, and z coordiantes 
        PlayerController.Instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        for(int i=0; i<playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_"+ playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }

            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

            //loads the stats, weapons/armor , exp, and level
            playerStats[i].playerLevel=PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_level");
            playerStats[i].currentEXP=PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentEXP");
            playerStats[i].currentHP= PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentHP");
        playerStats[i].currentMP= PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentMP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxHP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxMP");
            playerStats[i].str = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_strength");
            playerStats[i].def = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_defense");
            playerStats[i].wpn = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_weapon");
            playerStats[i].armor = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_armor");
            playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_weaponName");
            playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_armorName");

        } //for loop for load player stats end 

        for(int i=0; i<itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("itemInInventory" + i);

            numberOfHeld[i] = PlayerPrefs.GetInt("itemAmount_" + i); 
        }

    } // end of load data 




} //end of script 
