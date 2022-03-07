using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BattleTargetButton : MonoBehaviour {

    public string moveName;

    public int activeBattlerTarget;

    public Text targetName;

    public string highlightName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if the game object is selected, and the game object is a target button, then set the name in the enemy name box to the target buttons highlight name 
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<BattleTargetButton>() != null)
        {
            targetName.text = EventSystem.current.currentSelectedGameObject.GetComponent<BattleTargetButton>().highlightName;
     
        }


    }



    public void Press()
    {
        BattleManager.Instance.playerAttack(moveName, activeBattlerTarget); 
    }

}

    
    


