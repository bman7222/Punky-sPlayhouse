using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {

    public Image buttonImage;

    public Text amount;

    public Text itemName;

    public int buttonValue;

    public bool itemButtonPressed,shopBool;

    public GameObject itemSelectFirstButton, myButton; 

 

    // Use this for initialization
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        //SHOPS
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null && !GameMenu.Instance.Menu.activeInHierarchy)
        {

            //if the buy menu is open do this
            if (Shop.Instance.buyMenu.activeInHierarchy)
            {
                Shop.Instance.buyItemName.text = GameManager.Instance.getItemDetails(Shop.Instance.itemsForSale[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).name.ToString();
                Shop.Instance.buyItemDescription.text = GameManager.Instance.getItemDetails(Shop.Instance.itemsForSale[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).description.ToString();
                Shop.Instance.buyItemValue.text = GameManager.Instance.getItemDetails(Shop.Instance.itemsForSale[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).value.ToString();
            }

            //if the sell menu is open do this
            if (Shop.Instance.sellMenu.activeInHierarchy)
            {
                Shop.Instance.sellItemName.text = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).name.ToString();
                Shop.Instance.sellItemDescription.text = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).description.ToString();
                Shop.Instance.sellItemValue.text = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).value.ToString();
            }

        }
        //GAME ITEM MENU
        //if the game object is selected, and the game object is a target button, then set the name in the enemy name box to the target buttons highlight name 
        else if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null && GameMenu.Instance.Menu.activeInHierarchy)
        {
       
            GameMenu.Instance.itemName.text= GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).itemName.ToString();
            GameMenu.Instance.itemDescript.text = GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue]).description.ToString();


        }

       

    }

    public void Press()
    {

        //if game menu is open, then carry on 
        if (GameMenu.Instance.Menu.activeInHierarchy) {

        //if the value of the item button isn't null then the game menu will select it ad identify it usign the manager's button value to give it a name and description
        if (GameManager.Instance.itemsHeld[buttonValue] != "")
        {
                //get the details for that item
            GameMenu.Instance.selectItem(GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[buttonValue]));

                //the item button is confirmed pressed, this makes it so that the menu knows that an item butto was pressed and the "USE" button must be highlighted an the action menu must appear. THe back panel must be made false

                //set action menu as true
                GameMenu.Instance.itemActionMenu.SetActive(true);

                //set back panel as false
                GameMenu.Instance.itemBackPanel.SetActive(false);

                    //clear selected object 
                EventSystem.current.SetSelectedGameObject(null);

                    //set a new selected object
                EventSystem.current.SetSelectedGameObject(GameMenu.Instance.useButton);

       
               

            }
    }

        //if the shop is open then carry on
        if (Shop.Instance.shopMenu.activeInHierarchy)
        {
       
            //if the buy menu is open do this
            if (Shop.Instance.buyMenu.activeInHierarchy)
            {
                Shop.Instance.selectBuyItem(GameManager.Instance.getItemDetails(Shop.Instance.itemsForSale[buttonValue]));
            }

            //if the sell menu is open do this
            if (Shop.Instance.sellMenu.activeInHierarchy)
            {
                Shop.Instance.selectSellItem(GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[buttonValue]));
            }

        }

        if (BattleManager.Instance.battleActive == true)
        {

            GameMenu.Instance.selectItem(GameManager.Instance.getItemDetails(GameManager.Instance.itemsHeld[buttonValue]));

        }
}

} // end of script 
