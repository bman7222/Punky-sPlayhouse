using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private PlayerController targetObject;

    public static CameraController Instance;

    private Vector3 offset;

    //variables for audio manager
    public int musicToPlay;

    private bool musicStarted;

    public bool battleStarted;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.Instance.transform;

        offset = target.position - transform.position;

        Instance = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!target)
        {
            /*
            targetObject = FindObjectOfType<PlayerController>();

            target = targetObject.GetComponent<Transform>();

            offset = target.position - transform.position;

            transform.position = target.position - offset;*/
        }

        if (!battleStarted)
        {
            //transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
            transform.position = new Vector3(target.position.x, target.position.y +3, target.position.z - 7);
        }

        //if music hasnt stated, then start it
        if (!musicStarted)
        {
            musicStarted = true;
            AudioManager.Instance.playBGM(musicToPlay);
        }

    }
}
