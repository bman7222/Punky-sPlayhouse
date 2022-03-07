using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class GameMenu : MonoBehaviour {

    public GameObject Menu;

    public Selectable backButton,cancelButton;

    private CharStats [] playerStats ;

    public Text[] nameText, HPText, MPText, LVLText, EXPText;

    public Slider [] EXPSlider;

    public Image [] charImage;

    public GameObject[] charStatHolder;

    public GameObject[] windows;

    public GameObject[] statusButtons;

    public Text statusName, statusHP, statusMP, statusSTR, statusDEF, statusWPN, statusWPNPOW, statusARM, statusARMDEF, statusEXP;

    public Image statusImage;

    public List <ItemButton> itemButtons;

    public string selectedItem;

    public Item activeItem;

    public Text itemName, itemDescript, useButtonText;

    public static GameMenu Instance;

    public GameObject itemActionMenu, itemBackPanel, useButton,itemCharacterChoiceMenu,itemButtonParent;

    public ItemButton buttonTemplate;

    public IdSelect[] characterChoiceNames;

    public Text goldText;

    public GameObject pauseFirstButton, itemFirstButton, firstItemCharacter, statFirstButton;

 

 



    // Use this for initialization
    void Start () {

        Instance = this; 

	}
	
	// Update is called once per frame
	void Update () {

        //if button is clicked open or close menu 
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Menu.activeInHierarchy)
            {
                //menu is closed
                // Menu.SetActive(false);
                //game manager recgonzies menu is closed so player can move again
                //  GameManager.Instance.gameMenuOpen = false; 

                //menu is closed
                closeMenu();

                //time is back to normal
          
            }

            //if menu is open and the button is pressed then close menu 
           if(!Menu.activeInHierarchy && PlayerController.Instance.canMove == true)
            {
                //menu is active
                Menu.SetActive(true);
                //time is stopped
                Time.timeScale = 0f;

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);

                //main stats are updated
                updateMainStats(); 

                //game manager recgonzies menu is open so player can't move 
                GameManager.Instance.gameMenuOpen = true;

                //activate menu sound
                AudioManager.Instance.playSFX(5); 
            }
        }
		
	}

    public void updateMainStats()
    {
        //player stats are the stats
        playerStats = GameManager.Instance.playerStats; 

        //check stats
        for(int i=0; i<playerStats.Length; i++)
        {
            //check if character slot is activated, if not then leave blank
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                //turn on char slot
                charStatHolder[i].SetActive(true);

                //set the name to the name in the array 
                nameText[i].text = playerStats[i].charName;
                
                //set HP to the current HP/Max HP
                HPText[i].text= "HP: "+ playerStats[i].currentHP+"/"+playerStats[i].maxHP;

                //set MP to the current MP/Max MP
                MPText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;

                //set LVL to current LVL
                LVLText[i].text = "LVL " + playerStats[i].playerLevel;

                //Set exp to current EXP (current)/(needed)
                EXPText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].EXPtoNextLevel[playerStats[i].playerLevel];

                //set max exp of slider to max exp of array 
                EXPSlider[i].maxValue = playerStats[i].EXPtoNextLevel[playerStats[i].playerLevel];

                //set current exp of sldier to current exp of array
                EXPSlider[i].value = playerStats[i].currentEXP;

                //set image to image in array
                charImage[i].sprite = playerStats[i].charImage; 
            }
            else
            {
                //turn off char slot
                charStatHolder[i].SetActive(false);
            }


        } // end of for loop

        goldText.text = GameManager.Instance.currentGold.ToString(); 

    }

    //open button window
    public void toggleWindow(int windowNumber)  
    {

        updateMainStats();
        //check which button pressed
        for (int i=0; i<windows.Length; i++)
        {
            //open button based on its assigned number, the assign number can be checked in unity
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy); 
            } 

            //close all other menus
            else
            {
                windows[i].SetActive(false);

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }

        }

        //set the item character select menu to false 
        itemCharacterChoiceMenu.SetActive(false);

        //0 is the item menu, if the item menu is active, then the first button highlighted should be the first item 
        if (windows[0].activeInHierarchy)
        {
            if (GameMenu.Instance.gameObject.activeInHierarchy)
            {
                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(itemFirstButton);
            }
            

        }

        //1 is the stats menu, the first highlighted button should be p1    
        if (windows[1].activeInHierarchy)
        {

            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(statFirstButton);
        }


        }

        //close the menu
        public void closeMenu()
    {
        //set time back to normal
        Time.timeScale = 1f;

        //set all other menu windows to close
        for (int i=0; i<windows.Length; i++)
        {
            windows[i].SetActive(false); 
        }
        //close the menu 
        Menu.SetActive(false);

        //make sure that the game manager recgonzises menu is now clsoed
        GameManager.Instance.gameMenuOpen = false;

        //set the character chocie menu for item select to false
        itemCharacterChoiceMenu.SetActive(false); 
        
    }

    //for the stats menu buttons
    public void openStats()
    {
        //set player 0 as default
        statusCharacter(0);

        //update info shown 
        updateMainStats(); 

        //for every button set it active if the party slot is active and set the anem to the characters name
        for(int i=0; i<statusButtons.Length; i++)
        {

            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName; 
        }

    }

    //sets character stats
    public void statusCharacter(int selected)
    {
        //set char name
        statusName.text = playerStats[selected].charName;

        //set char HP 
        statusHP.text = ""+ playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;

        //set char MP
        statusMP.text = ""+ playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;

        //set char STR
        statusSTR.text = playerStats[selected].str.ToString();

        //set char DEF
        statusDEF.text = playerStats[selected].def.ToString();

        //if there is a weapon equipped then set the name to the weapon name otherwise display the default (none)
        if(playerStats[selected].equippedWeapon != "")
        {
            statusWPN.text = playerStats[selected].equippedWeapon;
        }

        //set weapon power
        statusWPNPOW.text = playerStats[selected].wpn.ToString();

        //if there is a armor equipped then set the name to the armor name otherwise display the default (none)
        if (playerStats[selected].equippedArmor != "")
        {
            statusARM.text = playerStats[selected].equippedArmor;
        }

        //set armor defense stat
        statusARMDEF.text = playerStats[selected].armor.ToString();

        //set exp to enxt lvl 
        //statusEXP.text = (playerStats[selected].EXPtoNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();

        //set char sprite
        statusImage.sprite = playerStats[selected].charImage;



    }

    /*
     * BUTTON
     * private text myText
     * 
     * public void setText(string text){
     * myText.text=textString;
     * 
     * }
     * 
     * public void OnClick(){
     * 
     * }
     * 
     * LIST CONTROL
     * private GameObject buttonTemplate;
     * private List<gameobject> buttons;
     * 
     * void generateButtons(){
     * 
     * }
     void start(){

        if(buttons>0){
        foreach(gameObject button i buttons){
        destory(button.gameObject);
        }

        butons.clear();
        }
    
     for(int i=1; i<=20l i++){
     GameObject button = Instatiate(buttonTemplate) as GameObject
     button.SetActive(true);
     button.GetComponent<ButtonListButton>().SetText("Button # "+i);
     button.transform.SetParent(buttonTemplate.transform.parent,false);
        }
        }

        void update(){

        }
      */

    public void showItems()

    {
        GameManager.Instance.sortItems();

        bool firstTime = true;

        //destroy all buttons
        if (itemButtons.Count > 0)
        {
            foreach (ItemButton button in itemButtons)
            {
                Destroy(button.gameObject);
            }
            itemButtons.Clear();
        }


        for(int i=0; i<GameManager.Instance.itemsHeld.Length; i++)
        {

            //if  item held isnt blank
            if(GameManager.Instance.itemsHeld[i] != "")
            {
                //make button
                ItemButton button = Instantiate(buttonTemplate) as ItemButton;

                //turn button on
                button.myButton.SetActive(true);

                //set parent
                button.myButton.transform.SetParent(itemButtonParent.transform,false);

                //call function to find item and go to the item script and grab the sprite and make the button have that sprite
                button.buttonImage.sprite = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[i]).itemSprite;

                //change amount to proper amount
                button.amount.text = GameManager.Instance.numberOfHeld[i].ToString();

                //change item name 
                button.itemName.text = GameManager.Instance.itemsHeld[i].ToString();

                //set value
                button.buttonValue = i;

                //add to list
                itemButtons.Add(button);

                //if this is the first run through, change the first item to the item first button
                if (firstTime)
                {
                    firstTime = false;

                    itemFirstButton = button.myButton;
                    

                }

                else
                {

                    //create a new navigation 
                    Navigation beforeButtonNav = itemButtons[i-1].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    beforeButtonNav.selectOnDown = itemButtons[i].GetComponent<Button>();

                    //create a new navigation 
                    Navigation currentButtonNav = itemButtons[i].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    currentButtonNav.selectOnUp = itemButtons[i-1].GetComponent<Button>();

                    //set the navigation of the buttons
                    itemButtons[i].GetComponent<Button>().navigation = currentButtonNav;
                    itemButtons[i-1].GetComponent<Button>().navigation = beforeButtonNav;
                }

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(itemFirstButton);

            }

            else
            {
                //turn off button and get rid of text

                //if there are items held
                if (GameManager.Instance.itemsHeld.Length > 0)
                {
                
                    //create a new navigation 
                    Navigation nav = itemButtons[i - 1].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    nav.selectOnDown = backButton;
                    //make sure the select up is the same
                    nav.selectOnUp = itemButtons[i - 1].GetComponent<Button>().navigation.selectOnUp;

                    //create a new navigation for the back button
                    Navigation backNav=backButton.GetComponent<Button>().navigation;
                    //change the select up 
                    backNav.selectOnUp = itemButtons[i - 1].GetComponent<Button>();

                    //set the navigation for the buttons
                    itemButtons[i - 1].GetComponent<Button>().navigation = nav;
                    backButton.GetComponent<Button>().navigation = backNav;

                }

                else
                {
                    //change the first selected object to the back panel
                    itemFirstButton = itemBackPanel;

                    //clear selected object 
                    EventSystem.current.SetSelectedGameObject(null);

                    //set a new selected object
                    EventSystem.current.SetSelectedGameObject(itemFirstButton);
                }
    
             
             /*itemButtons[i].gameObject.SetActive(false);
             itemButtons[i].buttonImage.gameObject.SetActive(false);
             itemButtons[i].amount.text = "";
             itemButtons[i].itemName.text = "";*/

            }

        }

    }

    public void selectItem(Item newItem)
    {
        activeItem = newItem;

        //if itemset use button text to use 
        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        
        //if armor or wepaon set use button text to equip
        if (activeItem.isArmor || activeItem.isWeapon){ 
        
            useButtonText.text = "Equip";
        }

        //set item name to name of activated item
        itemName.text =activeItem.itemName;

        //set item description to description of activated item
        itemDescript.text =activeItem.description; 
    }

    //for when the discard button is pressed
    public void discardItem()
    {
        //if the item ins't null use the remove item function from the game manager
        if (activeItem != null)
        {
            GameManager.Instance.removeItem(activeItem.itemName); 
        }

    }

    public void cancelSelection()
    {

        //set action menu as inactive
        itemActionMenu.SetActive(false);

        //opens character select panel
        itemBackPanel.SetActive(true);

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(itemFirstButton);

    }

    //opens the characer selecter when pressing the use item button 
    public void openItemCharacterChoice()
    {

        bool firstTime = true;

        //set action menu as inactive
        itemActionMenu.SetActive(false);

        //opens character select panel
        itemCharacterChoiceMenu.SetActive(true);

        List <IdSelect> activeCharacterNames = new List<IdSelect>();

        //for every character choice button, if the matching player stat is active in the hierarchy, then add it to the active characters, otherwise turn it off 
        for (int i = 0; i < characterChoiceNames.Length; i++)
        {
            if (GameManager.Instance.playerStats[i].gameObject.activeInHierarchy)
            {
                characterChoiceNames[i].GetComponent<Text>().text = GameManager.Instance.playerStats[i].charName;
                characterChoiceNames[i].gameObject.SetActive(true);
                activeCharacterNames.Add(characterChoiceNames[i]);
            }

            else
            {
                //turn off button
                characterChoiceNames[i].GetComponent<Text>().text = "";
                characterChoiceNames[i].gameObject.SetActive(false);
            }
        }

        //get names of all party members and display them accordingly
        for (int i=0; i<activeCharacterNames.Count; i++)
        {
            //if 1 character active
            if (activeCharacterNames.Count== 1)
            {
                //for every player stat, if the active characters name id matches the players name, set the text of the character button to the players name
                for (int j=0; j<GameManager.Instance.playerStats.Length;j++)
                {
                    if (activeCharacterNames[i].id == GameManager.Instance.playerStats[j].charName)
                    {
                        activeCharacterNames[i].GetComponent<Text>().text = GameManager.Instance.playerStats[0].charName;
                    }
                }

                //set nav 
                //create a new navigation 
                Navigation nav = activeCharacterNames[i].GetComponent<Button>().navigation;
                //change the select down on that new nav
                nav.selectOnRight = cancelButton;

                //set the navigation of the buttons
                activeCharacterNames[i].GetComponent<Button>().navigation= nav;

                //set nav 
                //create a new navigation 
                Navigation cancelNav = cancelButton.navigation;
                //change the select down on that new nav
                cancelNav.selectOnLeft = activeCharacterNames[i].GetComponent<Button>();

                //set the navigation of the buttons
                cancelButton.navigation = cancelNav;

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(activeCharacterNames[i].gameObject);
            }
            //if more active
            else
            {

             

                //for every player stat, if the active characters name id matches the players name, set the text of the character button to the players name
                for (int j = 0; j < GameManager.Instance.playerStats.Length; j++)
                {
                    if (activeCharacterNames[i].id == GameManager.Instance.playerStats[j].charName)
                    {
                        activeCharacterNames[i].GetComponent<Text>().text = GameManager.Instance.playerStats[j].charName;
                    }
                }

                //if this is the first run through
                if (firstTime)
                {
                    //set nav 
                    //create a new navigation 
                    Navigation nav2 = activeCharacterNames[i].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    nav2.selectOnRight = activeCharacterNames[i + 1].GetComponent<Button>();

                    //set the navigation of the buttons
                    activeCharacterNames[i].GetComponent<Button>().navigation = nav2;

                    firstTime = false;
                }

                //otherwise 
                else
                {
                    //set nav 
                    //create a new navigation 
                    Navigation nav = activeCharacterNames[i].GetComponent<Button>().navigation;
                    //change the select right on nav
                    nav.selectOnRight = cancelButton;
                    //change the select left
                    nav.selectOnLeft = activeCharacterNames[i - 1].GetComponent<Button>();

                    //set the navigation of the buttons
                    activeCharacterNames[i].GetComponent<Button>().navigation = nav;

                    //set nav 
                    //create a new navigation 
                    Navigation cancelNav = cancelButton.navigation;
                    //change the select down on that new nav
                    cancelNav.selectOnLeft = activeCharacterNames[i].GetComponent<Button>();

                    //set the navigation of the buttons
                    cancelButton.navigation = cancelNav;

                    //clear selected object 
                    EventSystem.current.SetSelectedGameObject(null);

                    //set a new selected object
                    EventSystem.current.SetSelectedGameObject(activeCharacterNames[0].gameObject);
                }

            }
        }

    }

    //close the character select from using an item
    public void closeItemCharacterChoice()
    {

        itemCharacterChoiceMenu.SetActive(false);

        //set action menu as inactive
        itemActionMenu.SetActive(true);

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(itemFirstButton);

    }

    //use the item on the selected character and close the character choice menu
    public void useItem(int selectChar)
    {
        activeItem.Use(selectChar);

        closeItemCharacterChoice(); 
    }

    //sets correct highlight when returning to main menu
    public void returnToMainMenu()
    {

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

    }

    //saves game data and quest data
    public void saveGame()
    {
        GameManager.Instance.saveData();
        QuestManager.Instance.saveQuestData(); 
    }

    public void playButtonSound()
    {
        AudioManager.Instance.playSFX(0); 
    }

    public void quitGame()
    {
        Application.Quit(); 
    }

   
}
