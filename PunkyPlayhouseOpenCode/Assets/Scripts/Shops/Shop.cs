using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Shop : MonoBehaviour {

    public static Shop Instance;

    public GameObject shopMenu, buyMenu,sellMenu,buyMenuButton,sellMenuButton,buyFirstButton,sellFirstButton,confirmBuy,confirmSell;

    public GameObject buyButtonParent, sellButtonParent;

    public Text goldText;

    public ItemButton buybuttonTemplate,sellbuttonTemplate;

    public bool shopIsOpen;

    public string[] itemsForSale;

    public List<ItemButton> buyitemButtons;

    public List<ItemButton> sellitemButtons;

    public Item selectedItem;

    public Text buyItemName, buyItemDescription, buyItemValue;

    public Text sellItemName, sellItemDescription, sellItemValue;

    public bool waitTimer;

	// Use this for initialization
	void Start () {

        Instance = this; 

	}
	
	// Update is called once per frame
	void Update () {

        //opens the shop menu if not opened
        if(Input.GetKeyDown(KeyCode.L) && !shopMenu.activeInHierarchy)
        {
            openShop(); 
        }
		
	}

    //method that opens the shop  and sets shopisopen to true 
    public void openShop()
    {
        shopIsOpen = true;

        GameManager.Instance.shopActive = true; 

        shopMenu.SetActive(true);

        openBuyMenu(); 
         
        goldText.text = GameManager.Instance.currentGold.ToString();
    }

    //method that closes the shop menu and sets shopisopen to false
    public void closeShop()
    {

        //deactivate all shop related objects
        shopIsOpen = false;

        GameManager.Instance.shopActive = false;

        sellMenu.SetActive(false);
        buyMenu.SetActive(true);

        shopMenu.SetActive(false);

        //set timer to zero
        float timer = 0f;

        //increase timer by time
        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;
        }

        //once timer > 0.1 the wait timer is set to true
        waitTimer = true;
    }

    //method that opens buy menu when pressing buy button
    public void openBuyMenu()
    {
        //buyitemButtons[0].Press();

        bool firstTime = true;

        sellMenu.SetActive(false);
        buyMenu.SetActive(true);

        //destroy all buttons
        if (buyitemButtons.Count > 0)
        {
            foreach (ItemButton button in buyitemButtons)
            {
                Destroy(button.gameObject);
            }
            buyitemButtons.Clear();
        }


        for (int i = 0; i < Shop.Instance.itemsForSale.Length; i++)
        {

            //if  item held isnt blank
            if (Shop.Instance.itemsForSale[i] != "")
            {
                //make button
                ItemButton button = Instantiate(buybuttonTemplate) as ItemButton;

                //turn button on
                button.myButton.SetActive(true);

                //set parent
                button.myButton.transform.SetParent(buyButtonParent.transform, false);

                //call function to find item and go to the item script and grab the sprite and make the button have that sprite
                button.buttonImage.sprite = GameManager.Instance.getItemDetails(Shop.Instance.itemsForSale[i]).itemSprite;

                //change amount to proper amount
                button.amount.text = "";

                //change item name 
                button.itemName.text = Shop.Instance.itemsForSale[i];

                //set value
                button.buttonValue = i;

                //add to list
                buyitemButtons.Add(button);

                //if this is the first run through, change the first item to the item first button
                if (firstTime)
                {
                    firstTime = false;

                    buyFirstButton = button.myButton;


                }

                else
                {

                    //create a new navigation 
                    Navigation beforeButtonNav = buyitemButtons[i - 1].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    beforeButtonNav.selectOnDown = buyitemButtons[i].GetComponent<Button>();

                    //change the select left on that new nav
                    beforeButtonNav.selectOnLeft = buyMenuButton.GetComponent<Button>();

                    //create a new navigation 
                    Navigation currentButtonNav = buyitemButtons[i].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    currentButtonNav.selectOnUp = buyitemButtons[i - 1].GetComponent<Button>();
                    //change the select left on that new nav
                    currentButtonNav.selectOnLeft = buyMenuButton.GetComponent<Button>();

                    //set the navigation of the buttons
                    buyitemButtons[i].GetComponent<Button>().navigation = currentButtonNav;
                    buyitemButtons[i - 1].GetComponent<Button>().navigation = beforeButtonNav;
                }

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(buyFirstButton);

            }

            else
            {
                //turn off button and get rid of text

                //if there are items held
                if (Shop.Instance.itemsForSale.Length > 0)
                {

                    //create a new navigation 
                    Navigation nav = buyitemButtons[i - 1].GetComponent<Button>().navigation;

                    //make sure the select up is the same
                    nav.selectOnUp = buyitemButtons[i - 1].GetComponent<Button>().navigation.selectOnUp;

                    //set the navigation for the buttons
                    buyitemButtons[i - 1].GetComponent<Button>().navigation = nav;


                }

                else
                {
                    //change the first selected object to the back panel
                    buyFirstButton = buyMenuButton;

                    //clear selected object 
                    EventSystem.current.SetSelectedGameObject(null);

                    //set a new selected object
                    EventSystem.current.SetSelectedGameObject(buyFirstButton);
                }


                /*itemButtons[i].gameObject.SetActive(false);
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amount.text = "";
                itemButtons[i].itemName.text = "";*/

            }

        }
    } // end of buy menu

    //method that opens sell menu when pressing sell button
    public void openSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        showSellItems(); 

     
    } // end of sell menu

    private void showSellItems()
    {
        GameManager.Instance.sortItems();

        bool firstTime = true;

        //destroy all buttons
        if (sellitemButtons.Count > 0)
        {
            foreach (ItemButton button in sellitemButtons)
            {
                Destroy(button.gameObject);
            }
            sellitemButtons.Clear();
        }


        for (int i = 0; i < GameManager.Instance.itemsHeld.Length; i++)
        {

            //if  item held isnt blank
            if (GameManager.Instance.itemsHeld[i] != "")
            {
                //make button
                ItemButton button = Instantiate(sellbuttonTemplate) as ItemButton;

                //turn button on
                button.myButton.SetActive(true);

                //set parent
                button.myButton.transform.SetParent(sellButtonParent.transform, false);

                //call function to find item and go to the item script and grab the sprite and make the button have that sprite
                button.buttonImage.sprite = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[i]).itemSprite;

                //change amount to proper amount
                button.amount.text = GameManager.Instance.numberOfHeld[i].ToString();

                //change item name 
                button.itemName.text = GameManager.Instance.itemsHeld[i].ToString();

                //set value
                button.buttonValue = i;

                //add to list
                sellitemButtons.Add(button);

                //if this is the first run through, change the first item to the item first button
                if (firstTime)
                {
                    firstTime = false;

                    sellFirstButton = button.myButton;


                }

                else
                {

                    //create a new navigation 
                    Navigation beforeButtonNav = sellitemButtons[i - 1].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    beforeButtonNav.selectOnDown = sellitemButtons[i].GetComponent<Button>();

                    //change the select left on that new nav
                    beforeButtonNav.selectOnLeft = sellMenuButton.GetComponent<Button>();

                    //create a new navigation 
                    Navigation currentButtonNav = sellitemButtons[i].GetComponent<Button>().navigation;
                    //change the select down on that new nav
                    currentButtonNav.selectOnUp = sellitemButtons[i - 1].GetComponent<Button>();
                    //change the select left on that new nav
                    currentButtonNav.selectOnLeft = sellMenuButton.GetComponent<Button>();

                    //set the navigation of the buttons
                    sellitemButtons[i].GetComponent<Button>().navigation = currentButtonNav;
                    sellitemButtons[i - 1].GetComponent<Button>().navigation = beforeButtonNav;
                }

                //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(sellFirstButton);

            }

            else
            {
                //turn off button and get rid of text

                //if there are items held
                if (GameManager.Instance.itemsHeld.Length > 0)
                {

                    //create a new navigation 
                    Navigation nav = sellitemButtons[i - 1].GetComponent<Button>().navigation;
    
                    //make sure the select up is the same
                    nav.selectOnUp = sellitemButtons[i - 1].GetComponent<Button>().navigation.selectOnUp;

                    //set the navigation for the buttons
                    sellitemButtons[i - 1].GetComponent<Button>().navigation = nav;


                }

                else
                {
                    //change the first selected object to the back panel
                    sellFirstButton = sellMenuButton;

                    //clear selected object 
                    EventSystem.current.SetSelectedGameObject(null);

                    //set a new selected object
                    EventSystem.current.SetSelectedGameObject(sellFirstButton);
                }


                /*itemButtons[i].gameObject.SetActive(false);
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amount.text = "";
                itemButtons[i].itemName.text = "";*/

            }

        }
    }

    //selects an item to buy
    public void selectBuyItem(Item buyItem)
    {

        //selects the item, name, description, and value
        selectedItem = buyItem;

        buyItemName.text = selectedItem.itemName;

        buyItemDescription.text = selectedItem.description;

        buyItemValue.text = "Value: " + selectedItem.value;

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(Shop.Instance.confirmBuy);
    }

    //selects an item to sell
    public void selectSellItem(Item sellItem)
    {
        //selects the item, name, description, and value

        selectedItem = sellItem;

        sellItemName.text = selectedItem.itemName;

        sellItemDescription.text = selectedItem.description;

        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value*0.5f).ToString();

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(Shop.Instance.confirmSell);

    }

    //purchases item from a shop
    public void buyItem()
    {
        if (selectedItem != null)
        {
            //if gold is greater than cost then subtract valu of item from the current gold and add item to inventory
            if (GameManager.Instance.currentGold >= selectedItem.value)
            {
                GameManager.Instance.currentGold -= selectedItem.value;

                GameManager.Instance.addItem(selectedItem.itemName);
            }
        }

        goldText.text = GameManager.Instance.currentGold.ToString(); 
    }

    public void sellItem()
    {

        if (selectedItem != null)
        {
            GameManager.Instance.currentGold += Mathf.FloorToInt(selectedItem.value * 0.5f);

            GameManager.Instance.removeItem(selectedItem.itemName);

        }

        goldText.text = GameManager.Instance.currentGold.ToString();

        showSellItems(); 

    }

} //end 
