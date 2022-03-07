using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour {

    private bool canPickUp; 

	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {

        if (canPickUp && Input.GetButtonDown("Submit") && PlayerController.Instance.canMove == true)
        {
            GameManager.Instance.addItem(GetComponent<Item>().itemName);
            Destroy(gameObject); 
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            canPickUp = true;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            canPickUp = false;
        }

    }
}
