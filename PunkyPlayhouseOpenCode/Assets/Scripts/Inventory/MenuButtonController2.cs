using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonController2 : MonoBehaviour
{
    public int index, maxIndex, onDeckIndex, numElements,currentIndex=0,lastCurrentIndex;

    public float multi;

    public static MenuButtonController2 Instance;

    public RectTransform rectTransform;
    public GameObject exampleButton;
    public float exampleButtonHeight;
    public Vector2 originalTransform;
    public float maxTransform;

    private bool firstTime = true;
    private bool firstTimeOpenMenu=true;

    public float originalHeight, exampleButtonMulti, timesToChangeSize;

    // Start is called before the first frame update
    void Start()
    {
        //first time set to true
        firstTime = true;

        //if this isnt the instance, make this the instance
        if (!Instance)
        {
            Instance = this;
        }

        //set the index, on deck index, max index, original transform, example button height,
        index = 0;

        onDeckIndex = 0;

        if (GameMenu.Instance.gameObject.activeInHierarchy)
        {

            maxIndex = GameMenu.Instance.itemButtons.Count;

        }

        if (Shop.Instance.sellMenu.activeInHierarchy)
        {

            maxIndex = GameManager.Instance.itemsHeld.Length;

        }

        if (Shop.Instance.buyMenu.activeInHierarchy)
        {

            maxIndex = Shop.Instance.itemsForSale.Length;

        }

        originalTransform = rectTransform.offsetMax;

        exampleButtonHeight = exampleButton.GetComponent<RectTransform>().sizeDelta.y;

        //set the original height, exampleButtonMulti, timesToChangeSize, and max transform

        originalHeight = originalTransform.y*2;

        exampleButtonMulti = (exampleButtonHeight * multi);

        timesToChangeSize=(maxIndex) - (numElements);

        maxTransform =originalHeight +(exampleButtonMulti*timesToChangeSize);
    }

    public void OnEnable()
    {

        //if, this is not the first time opening the menu
        if (!firstTimeOpenMenu)
        {
            //set rect transform to the original
            rectTransform.offsetMax = originalTransform;
        }

        //set first time, original height, timestochangesize, examplebuttonmulti, and max transform, index, and onDeckIndex
        firstTime = true;
        index = 0;
        onDeckIndex = 0;

        originalHeight = originalTransform.y * 2;

        exampleButtonMulti = (exampleButtonHeight * multi);

        timesToChangeSize = (maxIndex) - (numElements);

        maxTransform = originalHeight + (exampleButtonMulti * timesToChangeSize);

            //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        if (GameMenu.Instance.gameObject.activeInHierarchy)
        {
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(GameMenu.Instance.itemFirstButton);
        }

        if (Shop.Instance.buyMenu.activeInHierarchy)
        {
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(Shop.Instance.buyFirstButton);
        }

        if (Shop.Instance.sellMenu.activeInHierarchy)
        {
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(Shop.Instance.sellFirstButton);
        }

        //set firstTimeOpenMenu to false
        firstTimeOpenMenu = false;
    }


    // Update is called once per frame
    void Update()
    {

        //if, the currently selected object is an item button
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null && GameMenu.Instance.Menu.activeInHierarchy)
        {
            //set the index to the item buttons button value
            index = EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue;
        }

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null && Shop.Instance.buyMenu.activeInHierarchy)
        {
            //set the index to the item buttons button value
            index = EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue;
        }

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null && Shop.Instance.sellMenu.activeInHierarchy)
        {
            //set the index to the item buttons button value
            index = EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>().buttonValue;
        }

        //The current index is the last highlighted item. 
        if (index != currentIndex)
        {
            //if this is not the first time, lastCurrentIdex is the item highlighted before the previously highlighted item, so 2 items ago
            //the previous item and the item before that are saved
            //first time is set to false
            if (!firstTime)
            {
                lastCurrentIndex = currentIndex;
            }
            firstTime = false;
            currentIndex = index;

            //if the index is greater then the number of elements, and the index is less then the max, and the current index is greater then the last current idex (this means the indexes are increasing)
            //Then begin the procedure to scroll down
            if(index > (numElements - 1) && index < maxIndex && currentIndex > lastCurrentIndex )
            {
                //if the newly added height would make it greater then the max height allowed, then dont change the height
                //otherwise do change it
                if (rectTransform.offsetMax.y + (exampleButtonHeight * multi)<maxTransform) {
                    rectTransform.offsetMax += new Vector2(0, (exampleButtonHeight) * multi);
                }
                //Ondeckindex is equal to the index minus the number of items supposed to be show. EX if at item 7 and there are 4 items supposed to be show, ondeckIndex will be 3
                onDeckIndex = index - (numElements - 1);

            }

            else if((index == (onDeckIndex - 1)) && index < maxIndex && currentIndex < lastCurrentIndex)
            {
                    rectTransform.offsetMax -= new Vector2(0, (exampleButtonHeight) * multi);
                    onDeckIndex -= 1;
                
            }
        }

        //if the index is at the first item, and the transform of the viewport is not where it is suppsoed to be, put it in the right place
        if (index == 0 && rectTransform.offsetMax != originalTransform)
        {
            rectTransform.offsetMax = originalTransform;
        }
    }
}

