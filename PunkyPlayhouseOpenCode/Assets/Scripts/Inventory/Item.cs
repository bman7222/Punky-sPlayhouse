using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [Header ("Item Type")]
    public bool isItem;

    public bool isWeapon;

    public bool isArmor;

    [Header ("Item Details")]
    public string itemName;

    public string description;

    public int value;

    public Sprite itemSprite;


    [Header ("Item Details")]
    public int amountToChange;

    public bool affectHP, affectMP, affectSTR;

    [Header ("weapon/armor details")]
    public int weaponSTR;

    public int armorDEF;

  




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //if use button is used on an item 
    public void Use(int charToUseOn)
    {

        //find the character with the matching number and add effect to them
        CharStats selectedChar = GameManager.Instance.playerStats[charToUseOn];

        //make sure item is an item
        if (isItem)
        {
            //add hp to current hp
            if (affectHP)
            {
                selectedChar.currentHP += amountToChange;

                if (selectedChar.currentHP >  selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            } //end of affect HP

            //add mp to cureent mp
            if (affectMP)
            {
                selectedChar.currentMP += amountToChange;

                if (selectedChar.currentMP < selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }//end of affect MP

            // raise str number 
            if (affectSTR)
            {
                selectedChar.str += amountToChange;
            }

        }// end of is item 

            //if is weapon affect strength stat and affect name 
            if (isWeapon)
            {
                if(selectedChar.equippedWeapon !="")
                {
                    GameManager.Instance.addItem(selectedChar.equippedWeapon); 
                }

                selectedChar.equippedWeapon = itemName;
                selectedChar.wpn = weaponSTR;
            } //end of is weapon

        //if is armor affect defense stat and set name 
        if (isArmor)
        {
            if (selectedChar.equippedArmor != "")
            {
                GameManager.Instance.addItem(selectedChar.equippedArmor);
            }

            selectedChar.equippedArmor = itemName;
            selectedChar.armor = armorDEF;
        } //end of is weapon


        GameManager.Instance.removeItem(itemName); 

      

    }// end of Use public void

} //end of script
