using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFX : MonoBehaviour {

    public float effectLength;

    public int soundEffect;

    public float waitTime = 0f; 

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

        //destorys object after effect length
        Destroy(gameObject, effectLength); 

	}

    private void LateUpdate()
    {

        //Once the object spawns the wait time will be zero and then play the sfx, then wait tiem gets set to 1 so it will have to wait unti wait time equals zero in order to loop the sfx again
        if (waitTime == 0)
        {
            AudioManager.Instance.playSFX(soundEffect);
        }

        waitTime = 1f; 

    }




}
