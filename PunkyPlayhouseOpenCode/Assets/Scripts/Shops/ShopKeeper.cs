using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour {

    private bool canOpen, waitTimerBool;

    public string[] itemsForSale = new string [40]; 



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

        //if button is pressed and shop not open then open the shop
	void Update () {

        //if canopen (in the zone) and the button is pressed, and can move, and shop isnt open, and the waitTimerBool is true
        if(canOpen && Input.GetButtonDown("Submit") && PlayerController.Instance.canMove==true && !Shop.Instance.shopMenu.activeInHierarchy && waitTimerBool)
        {
            //Set wait timers to false and open the menu
            waitTimerBool = false;

            Shop.Instance.waitTimer = false;

            Shop.Instance.itemsForSale = itemsForSale;

            Shop.Instance.openShop();
        }

        //if the shops wait timer is true, then the wait timer bool is true. The shops waitTimer is only true once the menu has closed
        if (Shop.Instance.waitTimer)
        {
            waitTimerBool = true;
        }
	}
    //makes sure player is in the zone to enter the shop

    private void OnTriggerEnter(Collider other)
    {
        //when a plater enters, can open and waitTimerBool are set to true
        if (other.tag == "Player")
        {
            canOpen = true;
            waitTimerBool = true;
        }

    }

    //makes sure shop can't be opened when out of the zone
    private void OnTriggerExit(Collider other)
    {
        //on exit, set variables to false
        if (other.tag == "Player")
        {
            canOpen = false;
            waitTimerBool = false;
        }

    }

}
