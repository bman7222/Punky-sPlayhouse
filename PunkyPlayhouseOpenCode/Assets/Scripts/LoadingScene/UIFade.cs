
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIFade : MonoBehaviour
{

    //image to fade to
    public Image fadeScreen;

    //check if game shoudl fade
    private bool shouldfadetoblack;
    private bool shouldfadefromblack;

    //fade speed variable
    public float fadespeed;

    //UI fade set
    public static UIFade Instance;


    // Use this for initialization
    void Start()
    {

        //make sure the black box si always set
        Instance = this;

        //DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if (shouldfadetoblack)
        { //make screen black slowly 
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.r, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadespeed * Time.deltaTime));

            //once full make false
            if (fadeScreen.color.a == 1f)
            {
                shouldfadetoblack = false;
            }

        }

        //make screen normal slowly
        if (shouldfadefromblack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.r, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadespeed * Time.deltaTime));

            //once full make false
            if (fadeScreen.color.a == 0f)
            {
                shouldfadefromblack = false;
            }

        }

    }

    //activate fade out
    public void fadetoBlack()
    {
        shouldfadetoblack = true;
        shouldfadefromblack = false;

    }


    //activate fade in
    public void fadefromBlack()
    {
        shouldfadefromblack = true;
        shouldfadetoblack = false;



    }




}

