using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNotification : MonoBehaviour {

    public float awakeTime;

    private float awakeCounter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if the awake counter is greater than zero, then count down
        if (awakeCounter > 0)
        {
            //makes counter go down
            awakeCounter -= Time.deltaTime;

            //if the awake counter is less than zero than deactivate the description box
            if (awakeCounter <= 0)
            {
                BattleManager.Instance.descriptionBox.SetActive(false);
            }

        }

	}

    //happens when the notification is activated. Turns on the descirption box and turns awake counter equal to awake time to start countdown
    public void Activate()
    {

        BattleManager.Instance.descriptionBox.SetActive(true);

        awakeCounter = awakeTime;



    }




}
