using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DamageNumber : MonoBehaviour {

    public Text damageText;

    public float lifetime = 1f;

    public float moveSpeed = 1f;

    public float placementJitter = 0.5f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //destroys after lifetime seconds
        Destroy(gameObject, lifetime);

        //moves up the text at movespeed
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f); 

	}

    public void setDamage (int damageAmount)
    {

        //sets text to the damage amount
        damageText.text = "-"+ damageAmount.ToString();

        //jitters the text from negative placement to positive placement
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter)+0.5f, 0f);

    }


}
