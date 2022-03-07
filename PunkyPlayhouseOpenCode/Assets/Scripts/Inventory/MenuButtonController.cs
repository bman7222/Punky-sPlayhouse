using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonController : MonoBehaviour
{
    public int index, maxIndex,onDeckIndex,numElements;

    public float multi;

    public static MenuButtonController Instance;

    public bool keyDown;
    public RectTransform rectTransform;
    public bool isPressUp, isPressDown, isPressConfirm;
    public int VerticalMovement;
    public GameObject exampleButton;
    public float exampleButtonHeight;
    public Vector2 originalTransform;

    // Timer controls
    private float startTime = 0f;
    private float timer = 0f;
    public float holdTime = 0.0f;
    private bool held = false;


    // Start is called before the first frame update
    void Start()
    {
        //rectTransform = GetComponent<RectTransform>();
        isPressUp = isPressDown = false;

        if (!Instance)
        {
            Instance = this;
        }

        index = 0;

        onDeckIndex = 0;

        maxIndex = GameMenu.Instance.itemButtons.Count;

        originalTransform = rectTransform.offsetMax;

        exampleButtonHeight = exampleButton.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void OnEnable()
    {

        if (index > (numElements - 1))
        {
            index = 0;

            onDeckIndex = 0;

            rectTransform.offsetMax = originalTransform;

            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);

            //set a new selected object
            EventSystem.current.SetSelectedGameObject(GameMenu.Instance.itemFirstButton);
        }

        else
        {
            index = 0;
            onDeckIndex = 0;
        }
    }

    public void onPressUp()
    {
        isPressUp = true;
    }

    public void onReleaseUp()
    {
        isPressUp = false;
    }

    public void onPressDown()
    {
        isPressDown = true;
    }

    public void onReleaseDown()
    {
        isPressDown = false;
    }

    public void onPressConfirm()
    {
        isPressConfirm = true;
    }

    public void onReleaseConfirm()
    {
        isPressConfirm = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (isPressUp)
        {
            VerticalMovement = 1;
        }

        if (isPressDown)
        {
            VerticalMovement = -1;
        }

        if (!isPressUp && !isPressDown)
        {
            VerticalMovement = 0;
        }

        if (Input.GetAxisRaw("Vertical") != 0 || VerticalMovement != 0)
        {


            float startTime = Time.time;

            Debug.Log("starting"+startTime);

            float timer = startTime;
        }

        if (Input.GetAxisRaw("Vertical") != 0 || VerticalMovement != 0 && held == false)
        {
            

            timer += (Time.deltaTime)*10;

            Debug.Log("processing" + timer);

            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timer > (startTime + holdTime))
            {

                Debug.Log("doing");
                held = true;
                ButtonHeld();
            }
        }

        // For single effects. Remove if not needed
        if (Input.GetAxisRaw("Vertical") == 0 || VerticalMovement == 0)
        {
            held = false;
        }

        //BIG
        if (Input.GetAxisRaw("Vertical") != 0 || VerticalMovement != 0)
        {
            if (!keyDown)
            {

                if (Input.GetAxisRaw("Vertical") < 0 || VerticalMovement < 0)
                {
                    //Debug.Log("GOING DOWN");
                    if (index < (maxIndex-1))
                    {
                        index++;

                        if (index > (numElements-1) && index < maxIndex)
                        {
                            rectTransform.offsetMax += new Vector2(0, (exampleButtonHeight)*multi);
                            onDeckIndex = index - (numElements - 1);
                        }
                    }
                    else
                    {
                        index = 0;
                        onDeckIndex = 0;
                        rectTransform.offsetMax = originalTransform;

                        //clear selected object 
                        EventSystem.current.SetSelectedGameObject(null);

                        //set a new selected object
                        EventSystem.current.SetSelectedGameObject(GameMenu.Instance.itemFirstButton);
                    }

                }

                else if (Input.GetAxisRaw("Vertical") > 0 || VerticalMovement > 0)
                {
                   // Debug.Log("GOING UP");
                    if (index > 0 )
                    {
                        index--;

                           // if ((index < (maxIndex - 1)) && index > 0 && index >= (numElements - 1) && index==onDeckIndex)
                           if(index==(onDeckIndex-1))
                            {
                            rectTransform.offsetMax -= new Vector2(0, (exampleButtonHeight) * multi);
                            onDeckIndex-=1;
                        }
                        
                    }
                    /*else
                    {
                        index = maxIndex;
                        rectTransform.offsetMax = new Vector2(0, (maxIndex - (numElements-1)) * exampleButton.GetComponent<RectTransform>().sizeDelta.y);
                    }*/
                }

                keyDown = true;
            }
        }

        else
        {
            keyDown = false;
        }
    }

    // Method called after held for required time
    void ButtonHeld()
    {
        Debug.Log("held for " + holdTime + " seconds");
    }
}
