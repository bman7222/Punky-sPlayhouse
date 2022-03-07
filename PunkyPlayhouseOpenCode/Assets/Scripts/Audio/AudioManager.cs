using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource[] SFX;

        public AudioSource[] BGM;

    public static AudioManager Instance;

	// Use this for initialization
	void Start () {

        Instance = this;

        //DontDestroyOnLoad(this.gameObject); 

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            playSFX(0);
            playBGM(3); 
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            playSFX(1);
            playBGM(2);
        }


    }

    //play an sfx by accessing its spot in the array and the playing it 
    public void playSFX(int soundToPlay)
    {
        //makes sure that the SFX is in the array
        if (soundToPlay < SFX.Length) { 

        SFX[soundToPlay].Play();
     }

    }

    //play a music track by accessing its spot in the array and playing it 
    public void playBGM(int musicToPlay)
    {
        //if two scenes have the same bgm, then do not stop 
        if (!BGM[musicToPlay].isPlaying) {

            //stops music to make sure that the bgms dont play over each other
            stopMusic();

            //makes sure the music track is in the array
            if (musicToPlay < BGM.Length)
            {

                BGM[musicToPlay].Play();

            }//end of second if 
        } // end of first if 
    }

    public void stopMusic()
    {

        for(int i=0; i<BGM.Length; i++)
        {
            BGM[i].Stop(); 
        }

    }

    public void pauseMusic()
    {

    

    }

    public void playMusic()
    {



    }

} // end script 
