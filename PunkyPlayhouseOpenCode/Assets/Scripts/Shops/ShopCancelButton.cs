using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ShopCancelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Press()
    {
        //SHOPS

        //if the buy menu is open do this
        if (Shop.Instance.buyMenu.activeInHierarchy)
            {
 
            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(Shop.Instance.buyFirstButton);
        }

            //if the sell menu is open do this
            if (Shop.Instance.sellMenu.activeInHierarchy)
            {
           
            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(Shop.Instance.sellFirstButton);
        }

    }
}
