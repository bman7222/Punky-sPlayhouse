using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleChar : MonoBehaviour {

    public bool isPlayer;

    public string charName, hasDied;

    public int currentHP, maxHP, currentMP, maxMP, str, def, wpnPower, armPower,ID;

    public string[] movesAvailible;

    public SpriteRenderer theSprite;

    private bool shouldGrey;

    private bool shouldDisappear; 

        private float fadeSpeed=1f;

    private bool revived; 





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if a player dies, make them turn gray
        if (shouldGrey)
        {
            //changes the color to grey, the color fades to gray
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 0.502f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 0.502f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0.502f, fadeSpeed* Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 1f, fadeSpeed * Time.deltaTime));

        }

        //if an enemy dies, make them disappear and fade
        if (shouldDisappear)
        {

            //changes the color to grey, the color fades to gray
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 0f, fadeSpeed * Time.deltaTime));

            //when the alpha equals zero, make the enemy disappear
            if (theSprite.color.a == 0)
            {
                //turn off object 
                gameObject.SetActive(false);

            }

            if (revived)
            {
                //changes the color to grey, the color fades to gray
              theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 1f, fadeSpeed * Time.deltaTime));

            }

        }




    }

    public void shouldFadePlayer()
    {

        shouldGrey = true;

    }

    public void shouldFadeEnemy()
    {

        shouldDisappear = true;

    }

    public void shouldRevive()
    {

        revived = true;

    }


} //end of script 


