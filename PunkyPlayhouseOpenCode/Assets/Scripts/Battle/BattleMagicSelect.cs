using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{

    public string spellName,targetText, effectText;

    public int spellCost;

    public Text nameText, costText,effectTextUI,targetTextUI;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if the game object is selected, and the game object is a battle magic select, then set the effect and target to the button's target and effect strings 
        if (EventSystem.current.currentSelectedGameObject != null &&
   EventSystem.current.currentSelectedGameObject.GetComponent<BattleMagicSelect>() != null)
        {
            effectTextUI.text = EventSystem.current.currentSelectedGameObject.GetComponent<BattleMagicSelect>().effectText;
            targetTextUI.text = EventSystem.current.currentSelectedGameObject.GetComponent<BattleMagicSelect>().targetText;
        }
    }

    public void Press()
    {

        //when button is pressed, clsoemagic menu, pp box, make MP go down by spell cost, and open the target menu with the selected spell
        if (BattleManager.Instance.activeBattlers[BattleManager.Instance.currentTurn].currentMP >= spellCost)
        {

            //opens target menu to select an enemy to hit
            BattleManager.Instance.openTargetMenu(spellName);

            //closes the magic menu
            BattleManager.Instance.magicMenu.SetActive(false);

            //closes the ppbox
            BattleManager.Instance.effectBox.SetActive(false);

            //makes MP go down by spell cost
            BattleManager.Instance.activeBattlers[BattleManager.Instance.currentTurn].currentMP -= spellCost;

       

        }

        else
        {
            //sets the etxt of the descirpption box
            BattleManager.Instance.descriptionText.text = "Not enough MP.";

            //activates the notification so the descirption box appears 
            BattleManager.Instance.battleNotice.Activate();

            //turns off the ppbox and magic menu 
            BattleManager.Instance.magicMenu.SetActive(false);
            BattleManager.Instance.effectBox.SetActive(false);

            //turn on battle menu 
            BattleManager.Instance.effectBox.SetActive(true);

            //select highlight
            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(BattleManager.Instance.firstBattleButton);
        }

    }



}