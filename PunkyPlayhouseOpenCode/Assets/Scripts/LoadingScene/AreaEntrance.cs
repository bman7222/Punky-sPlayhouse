using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string transitionName;

    // Start is called before the first frame update

    void Start()
    {

        if (transitionName == PlayerController.Instance.areaTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
        }

        UIFade.Instance.fadefromBlack();

        //StartCoroutine(LateStart(1f));
    }

    /*
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call

        //set players position to this objects position
        if (transitionName == PlayerController.Instance.areaTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
    }*/
}
